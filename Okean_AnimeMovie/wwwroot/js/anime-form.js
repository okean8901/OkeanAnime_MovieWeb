$(document).ready(function() {
    console.log('Anime form script loaded');
    
    // Cải thiện trải nghiệm chọn nhiều thể loại
    const genreSelect = $('#GenreIds');
    
    if (genreSelect.length > 0) {
        console.log('Found genre select element');
        
        // Hiển thị số lượng thể loại đã chọn
        function updateSelectedCount() {
            const selectedCount = genreSelect.val() ? genreSelect.val().length : 0;
            console.log('Selected genres count:', selectedCount);
            
            const helpText = genreSelect.siblings('.form-text');
            if (helpText.length > 0) {
                if (selectedCount > 0) {
                    helpText.html(`<span class="text-success">✓ Đã chọn ${selectedCount} thể loại</span>`);
                } else {
                    helpText.html('Bạn có thể chọn nhiều thể loại bằng cách giữ phím Ctrl (Windows) hoặc Cmd (Mac) và click vào các thể loại mong muốn.');
                }
            }
        }
        
        // Cập nhật khi thay đổi selection
        genreSelect.on('change', function() {
            console.log('Genre selection changed');
            updateSelectedCount();
        });
        
        // Khởi tạo count ban đầu
        updateSelectedCount();
    }
    
    // Validation: Đảm bảo ít nhất chọn 1 thể loại
    $('form').on('submit', function(e) {
        const selectedGenres = genreSelect.val();
        console.log('Form submit, selected genres:', selectedGenres);
        
        if (!selectedGenres || selectedGenres.length === 0) {
            e.preventDefault();
            alert('Vui lòng chọn ít nhất một thể loại!');
            genreSelect.focus();
            return false;
        }
    });
});
