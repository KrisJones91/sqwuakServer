-- CREATE TABLE IF NOT EXISTS accounts(
--   id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
--   name varchar(255) COMMENT 'User Name',
--   email varchar(255) COMMENT 'User Email',
--   picture varchar(255) COMMENT 'User Picture'
-- ) default charset utf8 COMMENT '';
-- CREATE TABLE posts(
--   id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'create time',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'update time',
--   title varchar(255) NOT NULL,
--   description varchar(255) NOT NULL,
--   img VARCHAR(255),
--   views int DEFAULT 0,
--   shares int DEFAULT 0,
--   saves int DEFAULT 0,
--   creatorId varchar(255) comment 'FK: Account Id',
--   FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
-- -- ) default charset utf8 comment '';
-- CREATE TABLE archives(
--   id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'create time',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'update time',
--   name varchar(255),
--   isPrivate TINYINT DEFAULT 0,
--   creatorId varchar(255) comment 'FK: Account Id',
--   FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
-- ) default charset utf8 comment '';
-- CREATE TABLE archivePosts(
--   id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'create time',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'update time',
--   postId int NOT NULL,
--   archiveId int NOT NULL,
--   creatorId varchar(255) comment 'FK: Account Id',
--   FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
-- ) default charset utf8 comment '';
-- CREATE TABLE comments(
--   id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'create time',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'update time',
--   body VARCHAR(255) NOT NULL,
--   likes int DEFAULT 0,
--   postId int NOT NULL,
--   creatorId VARCHAR(255),
--   FOREIGN KEY (creatorId) REFERENCES accounts(id),
--   FOREIGN KEY (postId) REFERENCES posts(id) ON DELETE CASCADE
-- ) default charset utf8 comment '';
-- DROP TABLE vaults;