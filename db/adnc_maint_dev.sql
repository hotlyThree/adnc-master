/*
Navicat MySQL Data Transfer

Source Server         : ADNC
Source Server Version : 50505
Source Host           : 110.41.128.244:13308
Source Database       : adnc_maint_dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2024-01-31 16:19:22
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for sys_config
-- ----------------------------
DROP TABLE IF EXISTS `sys_config`;
CREATE TABLE `sys_config` (
  `id` bigint(20) NOT NULL,
  `isdeleted` tinyint(1) NOT NULL DEFAULT 0,
  `name` varchar(64) NOT NULL COMMENT '参数名',
  `value` varchar(128) NOT NULL COMMENT '参数值',
  `description` varchar(256) NOT NULL COMMENT '备注',
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='系统参数';

-- ----------------------------
-- Records of sys_config
-- ----------------------------
INSERT INTO `sys_config` VALUES ('349713920067013', '0', 'adnc', 'aspdotnetcore.net', '', '1600000000000', '2022-11-04 14:49:37.094930', '1600000000000', '2022-12-29 18:24:11.287761');

-- ----------------------------
-- Table structure for sys_dictionary
-- ----------------------------
DROP TABLE IF EXISTS `sys_dictionary`;
CREATE TABLE `sys_dictionary` (
  `id` bigint(20) NOT NULL,
  `isdeleted` tinyint(1) NOT NULL DEFAULT 0,
  `name` varchar(64) NOT NULL,
  `ordinal` int(11) NOT NULL,
  `pid` bigint(20) NOT NULL,
  `value` varchar(16) NOT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='字典';

-- ----------------------------
-- Records of sys_dictionary
-- ----------------------------
INSERT INTO `sys_dictionary` VALUES ('1600000008500', '0', '商品状态', '0', '0', '', '1600000000000', '2021-02-09 01:14:13.888000', '1600000000000', '2022-11-04 14:49:12.308456');
INSERT INTO `sys_dictionary` VALUES ('1600000008501', '1', '销售中', '0', '1600000008500', '1000', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008502', '1', '下架中', '0', '1600000008500', '1008', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008600', '0', '订单状态', '0', '0', '', '1600000000000', '2021-02-09 01:14:13.888000', '1600000000000', '2021-02-09 13:39:25.888000');
INSERT INTO `sys_dictionary` VALUES ('1600000008601', '0', '创建中', '0', '1600000008600', '1000', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008602', '0', '待付款', '0', '1600000008600', '1008', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008603', '0', '付款中', '0', '1600000008600', '1016', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008604', '0', '待发货', '0', '1600000008600', '1040', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008605', '0', '待确认', '0', '1600000008600', '1048', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008606', '0', '待评价', '0', '1600000008600', '1056', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008607', '0', '完成', '0', '1600000008600', '1064', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008608', '0', '取消中', '0', '1600000008600', '1023', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008609', '0', '已取消', '0', '1600000008600', '1024', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('1600000008610', '0', '已删除', '0', '1600000008600', '1032', '1600000000000', '2021-02-09 01:14:13.888000', null, null);
INSERT INTO `sys_dictionary` VALUES ('349713813259717', '1', '销售中', '0', '1600000008500', '1000', '1600000000000', '2022-11-04 14:49:11.455529', '1600000000000', '2022-11-04 14:49:11.455856');
INSERT INTO `sys_dictionary` VALUES ('349713813259718', '1', '下架中', '0', '1600000008500', '1008', '1600000000000', '2022-11-04 14:49:11.455557', '1600000000000', '2022-11-04 14:49:11.455859');
INSERT INTO `sys_dictionary` VALUES ('349713818781125', '0', '销售中', '0', '1600000008500', '1000', '1600000000000', '2022-11-04 14:49:12.502198', '1600000000000', '2022-11-04 14:49:12.502237');
INSERT INTO `sys_dictionary` VALUES ('349713818781126', '0', '下架中', '0', '1600000008500', '1008', '1600000000000', '2022-11-04 14:49:12.502199', '1600000000000', '2022-11-04 14:49:12.502238');
INSERT INTO `sys_dictionary` VALUES ('349713839523269', '1', 'test', '0', '0', '', '1600000000000', '2022-11-04 14:49:17.372902', '1600000000000', '2022-11-04 14:49:29.207544');
INSERT INTO `sys_dictionary` VALUES ('349713868817861', '1', '', '1', '349713839523269', '11', '1600000000000', '2022-11-04 14:49:24.704619', '1600000000000', '2022-11-04 14:49:24.704635');
INSERT INTO `sys_dictionary` VALUES ('349713887999429', '1', '111', '1', '349713839523269', '11', '1600000000000', '2022-11-04 14:49:29.381272', '1600000000000', '2022-11-04 14:49:29.381414');
INSERT INTO `sys_dictionary` VALUES ('369230717660613', '1', 'oo', '0', '0', '', '1600000000000', '2022-12-29 18:23:40.271444', '1600000000000', '2022-12-29 18:23:40.273440');
INSERT INTO `sys_dictionary` VALUES ('496013940930757', '0', '123', '0', '0', '', '1600000000000', '2023-12-23 00:25:55.779650', '1600000000000', '2023-12-23 00:25:55.786855');

-- ----------------------------
-- Table structure for sys_eventtracker
-- ----------------------------
DROP TABLE IF EXISTS `sys_eventtracker`;
CREATE TABLE `sys_eventtracker` (
  `id` bigint(20) NOT NULL,
  `eventid` bigint(20) NOT NULL,
  `trackername` varchar(50) NOT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_sys_eventtracker_eventid_trackername` (`eventid`,`trackername`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='事件跟踪/处理信息';

-- ----------------------------
-- Records of sys_eventtracker
-- ----------------------------

-- ----------------------------
-- Table structure for sys_notice
-- ----------------------------
DROP TABLE IF EXISTS `sys_notice`;
CREATE TABLE `sys_notice` (
  `id` bigint(20) NOT NULL,
  `content` varchar(255) NOT NULL,
  `title` varchar(64) NOT NULL,
  `type` int(11) NOT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='通知';

-- ----------------------------
-- Records of sys_notice
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
INSERT INTO `__efmigrationshistory` VALUES ('20221220081838_Init2022122001', '6.0.6');
INSERT INTO `__efmigrationshistory` VALUES ('20221224034518_v1.0.0', '6.0.6');
