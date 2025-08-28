class ViewHistoryTracker {
    constructor() {
        this.currentTime = 0;
        this.duration = 0;
        this.isTracking = false;
        this.trackInterval = null;
        this.animeId = null;
        this.episodeId = null;
        this.userId = null;
        this.lastUpdateTime = 0;
        this.updateInterval = 10000; // Cập nhật mỗi 10 giây
    }

    initialize(animeId, episodeId, userId) {
        this.animeId = animeId;
        this.episodeId = episodeId;
        this.userId = userId;
        
        // Lấy video element
        const video = document.querySelector('video');
        if (video) {
            this.setupVideoTracking(video);
        } else {
            // Nếu không có video element (có thể là embed), tạo tracking cơ bản
            this.setupBasicTracking();
        }
    }

    setupVideoTracking(video) {
        video.addEventListener('loadedmetadata', () => {
            this.duration = video.duration;
            console.log('Video loaded, duration:', this.duration);
        });

        video.addEventListener('play', () => {
            this.startTracking();
        });

        video.addEventListener('pause', () => {
            this.stopTracking();
            this.updateViewHistory();
        });

        video.addEventListener('ended', () => {
            this.stopTracking();
            this.updateViewHistory(true); // Đánh dấu hoàn thành
        });

        video.addEventListener('timeupdate', () => {
            this.currentTime = video.currentTime;
        });

        // Theo dõi khi người dùng rời khỏi trang
        window.addEventListener('beforeunload', () => {
            this.updateViewHistory();
        });

        // Theo dõi khi tab không active
        document.addEventListener('visibilitychange', () => {
            if (document.hidden) {
                this.stopTracking();
                this.updateViewHistory();
            } else {
                this.startTracking();
            }
        });
    }

    setupBasicTracking() {
        // Tracking cơ bản cho embed videos
        this.startTracking();
        
        // Cập nhật mỗi 30 giây
        setInterval(() => {
            this.currentTime += 30;
            this.updateViewHistory();
        }, 30000);
    }

    startTracking() {
        if (!this.isTracking) {
            this.isTracking = true;
            this.lastUpdateTime = Date.now();
            
            this.trackInterval = setInterval(() => {
                this.currentTime += 1;
                
                // Cập nhật lịch sử xem mỗi 10 giây
                if (Date.now() - this.lastUpdateTime >= this.updateInterval) {
                    this.updateViewHistory();
                    this.lastUpdateTime = Date.now();
                }
            }, 1000);
            
            console.log('Started tracking view history');
        }
    }

    stopTracking() {
        if (this.isTracking) {
            this.isTracking = false;
            if (this.trackInterval) {
                clearInterval(this.trackInterval);
                this.trackInterval = null;
            }
            console.log('Stopped tracking view history');
        }
    }

    updateViewHistory(isCompleted = false) {
        if (!this.animeId || !this.episodeId || !this.userId) {
            console.warn('Missing required data for view history update');
            return;
        }

        const viewHistoryData = {
            animeId: this.animeId,
            episodeId: this.episodeId,
            watchDuration: Math.floor(this.currentTime),
            isCompleted: isCompleted
        };

        fetch('/ViewHistory/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(viewHistoryData)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                console.log('View history updated successfully');
            } else {
                console.error('Failed to update view history:', data.message);
            }
        })
        .catch(error => {
            console.error('Error updating view history:', error);
        });
    }

    getCurrentProgress() {
        if (this.duration > 0) {
            return (this.currentTime / this.duration) * 100;
        }
        return 0;
    }

    getFormattedTime(seconds) {
        const hours = Math.floor(seconds / 3600);
        const minutes = Math.floor((seconds % 3600) / 60);
        const secs = Math.floor(seconds % 60);
        
        if (hours > 0) {
            return `${hours}:${minutes.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
        }
        return `${minutes}:${secs.toString().padStart(2, '0')}`;
    }
}

// Khởi tạo global instance
window.viewHistoryTracker = new ViewHistoryTracker();

// Hàm để khởi tạo tracking từ view
function initializeViewHistoryTracking(animeId, episodeId, userId) {
    if (window.viewHistoryTracker) {
        window.viewHistoryTracker.initialize(animeId, episodeId, userId);
    }
}

// Hàm để dừng tracking
function stopViewHistoryTracking() {
    if (window.viewHistoryTracker) {
        window.viewHistoryTracker.stopTracking();
        window.viewHistoryTracker.updateViewHistory();
    }
}

// Hàm để đánh dấu hoàn thành
function markEpisodeCompleted() {
    if (window.viewHistoryTracker) {
        window.viewHistoryTracker.updateViewHistory(true);
    }
}
