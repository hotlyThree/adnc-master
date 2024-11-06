/*
Navicat MySQL Data Transfer

Source Server         : ADNC
Source Server Version : 50505
Source Host           : 110.41.128.244:13308
Source Database       : adnc_ord_dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2024-01-31 16:19:37
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
-- Table structure for order
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order` (
  `id` bigint(20) NOT NULL,
  `customerid` bigint(20) NOT NULL COMMENT '客户Id',
  `amount` decimal(18,4) NOT NULL COMMENT '订单金额',
  `remark` varchar(64) DEFAULT NULL COMMENT '备注',
  `statuscode` int(11) NOT NULL,
  `statuschangesreason` varchar(32) DEFAULT NULL,
  `receivername` varchar(16) NOT NULL,
  `receiverphone` varchar(11) NOT NULL,
  `receiveraddress` varchar(64) NOT NULL,
  `rowversion` timestamp(6) NOT NULL DEFAULT current_timestamp(6) ON UPDATE current_timestamp(6),
  `createby` bigint(20) NOT NULL,
  `createtime` datetime(6) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='订单';

-- ----------------------------
-- Records of order
-- ----------------------------

-- ----------------------------
-- Table structure for orderitem
-- ----------------------------
DROP TABLE IF EXISTS `orderitem`;
CREATE TABLE `orderitem` (
  `id` bigint(20) NOT NULL,
  `orderid` bigint(20) NOT NULL COMMENT '订单Id',
  `producid` bigint(20) NOT NULL,
  `productname` varchar(64) NOT NULL,
  `productprice` decimal(18,4) NOT NULL,
  `count` int(11) NOT NULL COMMENT '数量',
  PRIMARY KEY (`id`),
  KEY `ix_orderitem_orderid` (`orderid`),
  CONSTRAINT `fk_orderitem_order_orderid` FOREIGN KEY (`orderid`) REFERENCES `order` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='订单条目';

-- ----------------------------
-- Records of orderitem
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
INSERT INTO `__efmigrationshistory` VALUES ('20230405141809_Init', '6.0.6');
INSERT INTO `__efmigrationshistory` VALUES ('20231120094058_Update2021030401', '6.0.6');
