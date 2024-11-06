/*
Navicat MySQL Data Transfer

Source Server         : ADNC
Source Server Version : 50505
Source Host           : 110.41.128.244:13308
Source Database       : adnc_cust_dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2024-01-31 16:18:56
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for cap.published
-- ----------------------------
DROP TABLE IF EXISTS `cap.published`;
CREATE TABLE `cap.published` (
  `Id` bigint(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Name` varchar(200) NOT NULL,
  `Content` longtext DEFAULT NULL,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(40) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ExpiresAt` (`ExpiresAt`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of cap.published
-- ----------------------------

-- ----------------------------
-- Table structure for cap.received
-- ----------------------------
DROP TABLE IF EXISTS `cap.received`;
CREATE TABLE `cap.received` (
  `Id` bigint(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Name` varchar(400) NOT NULL,
  `Group` varchar(200) DEFAULT NULL,
  `Content` longtext DEFAULT NULL,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ExpiresAt` (`ExpiresAt`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of cap.received
-- ----------------------------

-- ----------------------------
-- Table structure for customer
-- ----------------------------
DROP TABLE IF EXISTS `customer`;
CREATE TABLE `customer` (
  `id` bigint(20) NOT NULL,
  `account` varchar(16) NOT NULL,
  `password` varchar(32) NOT NULL,
  `nickname` varchar(16) NOT NULL,
  `realname` varchar(16) NOT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='客户表';

-- ----------------------------
-- Records of customer
-- ----------------------------
INSERT INTO `customer` VALUES ('495191021352517', 'hlf', '78B5D2D60D8F4B25C27F96F1FFD799D6', 'hlf', 'hlf', '1600000000000', '2023-12-20 16:37:27.599384', '1600000000000', '2023-12-20 16:37:27.602998');

-- ----------------------------
-- Table structure for customerfinance
-- ----------------------------
DROP TABLE IF EXISTS `customerfinance`;
CREATE TABLE `customerfinance` (
  `id` bigint(20) NOT NULL,
  `account` varchar(16) NOT NULL,
  `balance` decimal(18,4) NOT NULL,
  `rowversion` timestamp(6) NOT NULL DEFAULT current_timestamp(6) ON UPDATE current_timestamp(6),
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_customerfinance_customer_id` FOREIGN KEY (`id`) REFERENCES `customer` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='客户财务表';

-- ----------------------------
-- Records of customerfinance
-- ----------------------------
INSERT INTO `customerfinance` VALUES ('495191021352517', 'hlf', '0.0000', '2023-12-20 16:37:27.193520', '1600000000000', '2023-12-20 16:37:27.599422', '1600000000000', '2023-12-20 16:37:27.603032');

-- ----------------------------
-- Table structure for customertransactionlog
-- ----------------------------
DROP TABLE IF EXISTS `customertransactionlog`;
CREATE TABLE `customertransactionlog` (
  `id` bigint(20) NOT NULL,
  `customerid` bigint(20) NOT NULL,
  `account` varchar(16) NOT NULL,
  `exchangetype` int(11) NOT NULL,
  `exchagestatus` int(11) NOT NULL,
  `changingamount` decimal(18,4) NOT NULL,
  `amount` decimal(18,4) NOT NULL,
  `changedamount` decimal(18,4) NOT NULL,
  `remark` varchar(64) NOT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  PRIMARY KEY (`id`),
  KEY `ix_customertransactionlog_customerid` (`customerid`),
  CONSTRAINT `fk_customertransactionlog_customer_customerid` FOREIGN KEY (`customerid`) REFERENCES `customer` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='客户财务变动记录';

-- ----------------------------
-- Records of customertransactionlog
-- ----------------------------

-- ----------------------------
-- Table structure for eventtracker
-- ----------------------------
DROP TABLE IF EXISTS `eventtracker`;
CREATE TABLE `eventtracker` (
  `id` bigint(20) NOT NULL,
  `eventid` bigint(20) NOT NULL,
  `trackername` varchar(50) NOT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_eventtracker_eventid_trackername` (`eventid`,`trackername`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='事件跟踪/处理信息';

-- ----------------------------
-- Records of eventtracker
-- ----------------------------

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory` (
  `migrationid` varchar(150) NOT NULL,
  `productversion` varchar(32) NOT NULL,
  PRIMARY KEY (`migrationid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20221220082556_Init2022122001', '6.0.6');
