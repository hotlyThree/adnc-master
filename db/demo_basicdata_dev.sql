/*
Navicat MySQL Data Transfer

Source Server         : ADNC
Source Server Version : 50505
Source Host           : 110.41.128.244:13308
Source Database       : demo_basicdata_dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2024-01-31 16:20:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for basicdata_eventtracker
-- ----------------------------
DROP TABLE IF EXISTS `basicdata_eventtracker`;
CREATE TABLE `basicdata_eventtracker` (
  `id` bigint(20) NOT NULL,
  `eventid` bigint(20) NOT NULL,
  `trackername` varchar(50) NOT NULL,
  `createby` bigint(20) NOT NULL,
  `createtime` datetime(6) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_basicdata_eventtracker_eventid_trackername` (`eventid`,`trackername`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of basicdata_eventtracker
-- ----------------------------

-- ----------------------------
-- Table structure for basicdata_productcategory
-- ----------------------------
DROP TABLE IF EXISTS `basicdata_productcategory`;
CREATE TABLE `basicdata_productcategory` (
  `id` bigint(20) NOT NULL,
  `isdeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `isenable` tinyint(1) NOT NULL COMMENT '启用标识',
  `parentid` bigint(20) NOT NULL COMMENT '父节点',
  `layer` int(11) NOT NULL COMMENT '层级',
  `encode` varchar(50) NOT NULL COMMENT '分类编码',
  `fullname` varchar(50) NOT NULL COMMENT '分类名称',
  `description` varchar(320) NOT NULL COMMENT '描述',
  `createby` bigint(20) NOT NULL,
  `createtime` datetime(6) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of basicdata_productcategory
-- ----------------------------

-- ----------------------------
-- Table structure for basicdata_testgenerator
-- ----------------------------
DROP TABLE IF EXISTS `basicdata_testgenerator`;
CREATE TABLE `basicdata_testgenerator` (
  `id` bigint(20) NOT NULL,
  `isdeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '删除标识',
  `rowversion` timestamp(6) NOT NULL DEFAULT current_timestamp(6) ON UPDATE current_timestamp(6) COMMENT '并发控制列',
  `ordinal` int(11) NOT NULL COMMENT '排序字段',
  `isenable` tinyint(1) NOT NULL COMMENT '启用标识',
  `stringfiledallownull` varchar(50) DEFAULT NULL COMMENT '可空字符',
  `stringfilednotnull` varchar(50) NOT NULL COMMENT '不可空字符',
  `datetimeallownull` datetime(6) DEFAULT NULL COMMENT '可空日期',
  `datetimenotnull` datetime(6) NOT NULL COMMENT '不可空日期',
  `floatallownull` float DEFAULT NULL COMMENT '可空单精度',
  `floatnotnull` float NOT NULL COMMENT '不可空单精度',
  `doubleallownull` double DEFAULT NULL COMMENT '可空双精度',
  `doublenotnull` double NOT NULL COMMENT '不可空双精度',
  `decimalallownull` decimal(18,4) DEFAULT NULL COMMENT '可空Decimal',
  `decimalnotnull` decimal(18,4) NOT NULL COMMENT '不可空Decimal',
  `boolallownull` tinyint(1) DEFAULT NULL COMMENT '可空Bool',
  `boolnotnull` tinyint(1) NOT NULL COMMENT '不可空Bool',
  `intallownull` int(11) DEFAULT NULL COMMENT '可空Int',
  `intnotnull` int(11) NOT NULL COMMENT '不可空Int',
  `longalownull` bigint(20) DEFAULT NULL COMMENT '可空long',
  `longnotnull` bigint(20) NOT NULL COMMENT '不可空long',
  `createby` bigint(20) NOT NULL,
  `createtime` datetime(6) NOT NULL,
  `modifyby` bigint(20) DEFAULT NULL,
  `modifytime` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of basicdata_testgenerator
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
INSERT INTO `__efmigrationshistory` VALUES ('20231026075557_Update2021030401', '6.0.6');
INSERT INTO `__efmigrationshistory` VALUES ('20231026075946_Init20231026', '6.0.6');
