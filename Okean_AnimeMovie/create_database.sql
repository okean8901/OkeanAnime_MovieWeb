-- Script tạo database cho Okean Anime Movie
-- Chạy script này trong MySQL Workbench hoặc MySQL command line

-- Tạo database
CREATE DATABASE IF NOT EXISTS `Okean_AnimeMovie` 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

-- Sử dụng database
USE `Okean_AnimeMovie`;

-- Hiển thị thông tin database
SELECT 'Database Okean_AnimeMovie đã được tạo thành công!' AS Message;
