/*
Navicat MySQL Data Transfer

Source Server         : ADNC
Source Server Version : 50505
Source Host           : 110.41.128.244:13308
Source Database       : adnc_usr_dev

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2024-01-31 16:20:01
*/

SET FOREIGN_KEY_CHECKS=0;

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
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu` (
  `id` bigint(20) NOT NULL,
  `code` varchar(16) NOT NULL COMMENT '编号',
  `component` varchar(64) DEFAULT NULL COMMENT '組件配置',
  `hidden` tinyint(1) NOT NULL COMMENT '是否隐藏',
  `icon` varchar(16) DEFAULT NULL COMMENT '图标',
  `ismenu` tinyint(1) NOT NULL COMMENT '是否是菜单1:菜单,0:按钮',
  `isopen` tinyint(1) NOT NULL COMMENT '是否默认打开1:是,0:否',
  `levels` int(11) NOT NULL COMMENT '级别',
  `name` varchar(16) NOT NULL COMMENT '名称',
  `ordinal` int(11) NOT NULL COMMENT '序号',
  `pcode` varchar(16) NOT NULL COMMENT '父菜单编号',
  `pcodes` varchar(128) NOT NULL COMMENT '递归父级菜单编号',
  `status` tinyint(1) NOT NULL COMMENT '状态1:启用,0:禁用',
  `tips` varchar(32) DEFAULT NULL COMMENT '鼠标悬停提示信息',
  `url` varchar(64) NOT NULL COMMENT '链接',
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='菜单';

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES ('1600000000001', 'usr', 'layout', '0', 'peoples', '1', '0', '1', '用户中心', '0', '0', '[0],', '1', '', '/usr', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-12-29 18:25:00.626499');
INSERT INTO `sys_menu` VALUES ('1600000000003', 'maintain', 'layout', '0', 'operation', '1', '0', '1', '运维中心', '1', '0', '[0],', '1', '', '/maintain', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-11-04 15:00:36.204374');
INSERT INTO `sys_menu` VALUES ('1600000000004', 'user', 'views/usr/user/index', '0', 'user', '1', '0', '2', '用户管理', '0', 'usr', '[0],[usr]', '1', null, '/user', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-11-25 00:21:11.459114');
INSERT INTO `sys_menu` VALUES ('1600000000005', 'userAdd', null, '0', '', '0', '0', '3', '添加用户', '0', 'user', '[0],[usr][user]', '1', null, '/user/add', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-11-25 00:21:23.738379');
INSERT INTO `sys_menu` VALUES ('1600000000006', 'userEdit', null, '0', '', '0', '0', '3', '修改用户', '0', 'user', '[0],[usr][user]', '1', null, '/user/edit', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-11-25 00:21:44.038549');
INSERT INTO `sys_menu` VALUES ('1600000000007', 'userDelete', null, '0', null, '0', '0', '3', '删除用户', '0', 'user', '[0],[usr],[user],', '1', null, '/user/delete', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000008', 'userReset', null, '0', null, '0', '0', '3', '重置密码', '0', 'user', '[0],[usr],[user],', '1', null, '/user/reset', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000009', 'userFreeze', null, '0', '', '0', '0', '3', '冻结用户', '0', 'user', '[0],[usr][user]', '1', null, '/user/freeze', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-11-25 00:21:48.244064');
INSERT INTO `sys_menu` VALUES ('1600000000010', 'userUnfreeze', null, '0', null, '0', '0', '3', '解除冻结用户', '0', 'user', '[0],[usr],[user],', '1', null, '/user/unfreeze', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000011', 'userSetRole', null, '0', null, '0', '0', '3', '分配角色', '0', 'user', '[0],[usr],[user],', '1', null, '/user/setRole', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000012', 'role', 'views/usr/role/index', '0', 'people', '1', '0', '2', '角色管理', '0', 'usr', '[0],[usr]', '1', null, '/role', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000013', 'roleAdd', null, '0', null, '0', '0', '3', '添加角色', '0', 'role', '[0],[usr],[role],', '1', null, '/role/add', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000014', 'roleEdit', null, '0', null, '0', '0', '3', '修改角色', '0', 'role', '[0],[usr],[role],', '1', null, '/role/edit', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000015', 'roleDelete', null, '0', null, '0', '0', '3', '删除角色', '0', 'role', '[0],[usr],[role]', '1', null, '/role/delete', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000016', 'roleSetAuthority', null, '0', null, '0', '0', '3', '配置权限', '0', 'role', '[0],[usr],[role],', '1', null, '/role/setAuthority', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000017', 'menu', 'views/usr/menu/index', '0', 'menu', '1', '0', '2', '菜单管理', '0', 'usr', '[0],[usr]', '1', null, '/menu', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000018', 'menuAdd', null, '0', null, '0', '0', '3', '添加菜单', '0', 'menu', '[0],[usr],[menu],', '1', null, '/menu/add', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000019', 'menuEdit', null, '0', null, '0', '0', '3', '修改菜单', '0', 'menu', '[0],[usr],[menu],', '1', null, '/menu/edit', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000020', 'menuDelete', null, '0', null, '0', '0', '3', '删除菜单', '0', 'menu', '[0],[usr],[menu],', '1', null, '/menu/remove', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000021', 'dept', 'views/usr/dept/index', '0', 'dept', '1', '0', '2', '部门管理', '0', 'usr', '[0],[usr],', '1', null, '/dept', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000022', 'dict', 'views/maintain/dict/index', '0', 'dict', '1', '0', '2', '字典管理', '0', 'maintain', '[0],[maintain]', '1', '', '/dict', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-12-29 18:25:15.231079');
INSERT INTO `sys_menu` VALUES ('1600000000023', 'deptEdit', null, '0', null, '0', '0', '3', '修改部门', '0', 'dept', '[0],[usr],[dept],', '1', null, '/dept/update', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000024', 'deptDelete', null, '0', null, '0', '0', '3', '删除部门', '0', 'dept', '[0],[usr],[dept],', '1', null, '/dept/delete', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000025', 'dictAdd', null, '0', '', '0', '0', '3', '添加字典', '0', 'dict', '[0],[maintain],[dict]', '1', null, '/dict/add', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-08-15 11:03:42.351551');
INSERT INTO `sys_menu` VALUES ('1600000000026', 'dictEdit', null, '0', null, '0', '0', '3', '修改字典', '0', 'dict', '[0],[maintain],[dict],', '1', null, '/dict/update', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000027', 'dictDelete', null, '0', null, '0', '0', '3', '删除字典', '0', 'dict', '[0],[maintain],[dict],', '1', null, '/dict/delete', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000028', 'deptList', null, '0', null, '0', '0', '3', '部门列表', '0', 'dept', '[0],[usr],[dept],', '1', null, '/dept/list', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000030', 'dictList', null, '0', null, '0', '0', '3', '字典列表', '0', 'dict', '[0],[maintain],[dict],', '1', null, '/dict/list', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000032', 'deptAdd', null, '0', null, '0', '0', '3', '添加部门', '0', 'dept', '[0],[usr],[dept],', '1', null, '/dept/add', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000033', 'cfg', 'views/maintain/cfg/index', '0', 'cfg', '1', '0', '2', '参数管理', '0', 'maintain', '[0],[maintain]', '1', '', '/cfg', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-12-29 18:25:21.399448');
INSERT INTO `sys_menu` VALUES ('1600000000034', 'cfgAdd', null, '0', null, '0', '0', '3', '添加系统参数', '0', 'cfg', '[0],[maintain],[cfg],', '1', null, '/cfg/add', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000035', 'cfgEdit', null, '0', null, '0', '0', '3', '修改系统参数', '0', 'cfg', '[0],[maintain],[cfg],', '1', null, '/cfg/update', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000036', 'cfgDelete', null, '0', null, '0', '0', '3', '删除系统参数', '0', 'cfg', '[0],[maintain],[cfg],', '1', null, '/cfg/delete', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000048', 'opsLog', 'views/maintain/opslog/index', '0', 'log', '1', '0', '2', '操作日志', '0', 'maintain', '[0],[maintain]', '1', '', '/opslog', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-12-29 18:25:37.792634');
INSERT INTO `sys_menu` VALUES ('1600000000049', 'loginLog', 'views/maintain/loginlog/index', '0', 'logininfor', '1', '0', '2', '登录日志', '0', 'maintain', '[0],[maintain]', '1', '', '/loginlog', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-12-29 18:25:43.303822');
INSERT INTO `sys_menu` VALUES ('1600000000054', 'druid', 'layout', '0', 'link', '1', '0', '2', '性能检测', '0', 'maintain', '[0],[maintain]', '1', null, 'http://skywalking.aspdotnetcore.net', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-08-15 11:07:15.272200');
INSERT INTO `sys_menu` VALUES ('1600000000055', 'swagger', 'views/maintain/swagger/index', '0', 'swagger', '1', '0', '2', '接口文档', '0', 'maintain', '[0],[maintain]', '1', null, '/swagger', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-11-25 19:30:04.982175');
INSERT INTO `sys_menu` VALUES ('1600000000071', 'nlogLog', 'layout', '0', 'logininfor', '1', '0', '2', 'Nlog日志', '0', 'maintain', '[0],[maintain]', '1', null, 'http://loki.aspdotnetcore.net', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-08-15 11:07:03.454784');
INSERT INTO `sys_menu` VALUES ('1600000000072', 'health', 'layout', '0', 'monitor', '1', '0', '2', '健康检测', '0', 'maintain', '[0],[maintain]', '1', '', 'http://prometheus.aspdotnetcore.net', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-12-29 18:23:10.485071');
INSERT INTO `sys_menu` VALUES ('1600000000073', 'menuList', null, '0', '', '0', '0', '3', '菜单列表', '0', 'menu', '[0],[usr][menu]', '1', null, '/menu/list', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000074', 'roleList', null, '0', '', '0', '0', '3', '角色列表', '1', 'role', '[0],[usr][role]', '1', null, '/role/list', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2021-02-08 23:29:43.628024');
INSERT INTO `sys_menu` VALUES ('1600000000075', 'userList', null, '0', '', '0', '0', '3', '用户列表', '0', 'user', '[0],[usr][user]', '1', null, 'user/list', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000076', 'cfgList', null, '0', '', '0', '0', '3', '系统参数列表', '0', 'cfg', '[0],[maintain][cfg]', '1', null, '/cfg/list', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2020-08-08 08:08:08.888888');
INSERT INTO `sys_menu` VALUES ('1600000000078', 'eventBus', 'layout', '0', 'server', '1', '0', '2', 'EventBus', '0', 'maintain', '[0],[maintain]', '1', null, 'http://114.132.157.167:8888/cus/cap/', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-08-15 11:05:57.235325');

-- ----------------------------
-- Table structure for sys_organization
-- ----------------------------
DROP TABLE IF EXISTS `sys_organization`;
CREATE TABLE `sys_organization` (
  `id` bigint(20) NOT NULL,
  `fullname` varchar(32) NOT NULL,
  `ordinal` int(11) NOT NULL,
  `pid` bigint(20) DEFAULT NULL,
  `pids` varchar(80) NOT NULL,
  `simplename` varchar(16) NOT NULL,
  `tips` varchar(64) DEFAULT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='部门';

-- ----------------------------
-- Records of sys_organization
-- ----------------------------
INSERT INTO `sys_organization` VALUES ('1600000001000', '总公司', '0', '0', '[0],', '总公司', null, '1600000000000', '2020-11-24 01:22:27.000000', '1600000000000', '2020-11-25 18:30:11.883723');
INSERT INTO `sys_organization` VALUES ('1606155294001', '财务部', '2', '1600000001000', '[0],[1600000001000],', '财务部', '', '1600000000000', '2020-11-24 02:14:54.675010', '1600000000000', '2022-12-29 18:23:16.913566');
INSERT INTO `sys_organization` VALUES ('1606155335002', '研发部', '1', '1600000001000', '[0],[1600000001000],', '研发部', null, '1600000000000', '2020-11-24 02:15:35.476720', '1600000000000', '2021-02-08 22:46:38.866773');
INSERT INTO `sys_organization` VALUES ('1606155393003', 'csharp组', '6', '1606155335002', '[0],[1600000001000],[1606155335002],', 'csharp组', null, '1600000000000', '2020-11-24 02:16:33.336059', '1600000000000', '2021-02-08 23:40:42.477252');
INSERT INTO `sys_organization` VALUES ('1606155436004', 'go组', '3', '1606155335002', '[0],[1600000001000],[1606155335002],', 'go组', null, '1600000000000', '2021-02-02 12:45:35.079665', '1600000000000', '2021-02-08 23:19:00.342879');
INSERT INTO `sys_organization` VALUES ('1612797557001', 'java组', '1', '1606155335002', '[0],[1600000001000],[1606155335002],', 'java组', null, '1600000000000', '2021-02-08 23:19:17.938562', null, null);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role` (
  `id` bigint(20) NOT NULL,
  `deptid` bigint(20) DEFAULT NULL,
  `name` varchar(32) NOT NULL,
  `ordinal` int(11) NOT NULL,
  `pid` bigint(20) DEFAULT NULL,
  `tips` varchar(64) DEFAULT NULL,
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='角色';

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES ('1600000000010', null, '系统管理员', '0', null, 'administrator', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2021-02-08 23:39:16.907337');
INSERT INTO `sys_role` VALUES ('1606156061057', null, '只读用户', '1', null, 'readonly', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-11-04 14:33:16.159422');

-- ----------------------------
-- Table structure for sys_rolerelation
-- ----------------------------
DROP TABLE IF EXISTS `sys_rolerelation`;
CREATE TABLE `sys_rolerelation` (
  `id` bigint(20) NOT NULL,
  `menuid` bigint(20) NOT NULL,
  `roleid` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `ix_sys_rolerelation_menuid` (`menuid`),
  CONSTRAINT `fk_sys_rolerelation_sys_menu_menuid` FOREIGN KEY (`menuid`) REFERENCES `sys_menu` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='菜单角色关系';

-- ----------------------------
-- Records of sys_rolerelation
-- ----------------------------
INSERT INTO `sys_rolerelation` VALUES ('1606193510001', '1600000000001', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510002', '1600000000004', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510003', '1600000000005', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510004', '1600000000006', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510005', '1600000000007', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510006', '1600000000008', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510007', '1600000000009', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510008', '1600000000010', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510009', '1600000000011', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510010', '1600000000075', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510011', '1600000000012', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510012', '1600000000013', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510013', '1600000000014', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510014', '1600000000015', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510015', '1600000000016', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510016', '1600000000074', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510017', '1600000000017', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510018', '1600000000018', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510019', '1600000000019', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510020', '1600000000020', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510021', '1600000000073', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510022', '1600000000021', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510023', '1600000000023', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510024', '1600000000024', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510025', '1600000000028', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510026', '1600000000032', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510027', '1600000000003', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510028', '1600000000022', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510029', '1600000000025', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510030', '1600000000026', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510031', '1600000000027', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510032', '1600000000030', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510033', '1600000000033', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510034', '1600000000034', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510035', '1600000000035', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510036', '1600000000036', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510037', '1600000000076', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510044', '1600000000048', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510045', '1600000000049', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510046', '1600000000054', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510047', '1600000000055', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510048', '1600000000071', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510049', '1600000000072', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1606193510050', '1600000000078', '1600000000010');
INSERT INTO `sys_rolerelation` VALUES ('1610294626091', '1600000000003', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626092', '1600000000022', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626093', '1600000000025', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626094', '1600000000026', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626095', '1600000000027', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626096', '1600000000030', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626097', '1600000000033', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626098', '1600000000034', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626099', '1600000000035', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626100', '1600000000036', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626101', '1600000000076', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626108', '1600000000048', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626109', '1600000000049', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626110', '1600000000054', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626111', '1600000000055', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626112', '1600000000071', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626113', '1600000000072', '1606156061057');
INSERT INTO `sys_rolerelation` VALUES ('1610294626114', '1600000000078', '1606156061057');

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `id` bigint(20) NOT NULL,
  `isdeleted` tinyint(1) NOT NULL DEFAULT 0,
  `account` varchar(16) NOT NULL COMMENT '账号',
  `avatar` varchar(64) NOT NULL COMMENT '头像路径',
  `birthday` datetime(6) DEFAULT NULL COMMENT '生日',
  `deptid` bigint(20) DEFAULT NULL COMMENT '部门Id',
  `email` varchar(32) NOT NULL COMMENT 'email',
  `name` varchar(16) NOT NULL COMMENT '姓名',
  `password` varchar(32) NOT NULL COMMENT '密码',
  `phone` varchar(11) NOT NULL COMMENT '手机号',
  `roleids` varchar(72) NOT NULL COMMENT '角色id列表，以逗号分隔',
  `salt` varchar(6) NOT NULL COMMENT '密码盐',
  `sex` int(11) NOT NULL COMMENT '性别',
  `status` int(11) NOT NULL COMMENT '状态',
  `createby` bigint(20) NOT NULL COMMENT '创建人',
  `createtime` datetime(6) NOT NULL COMMENT '创建时间/注册时间',
  `modifyby` bigint(20) DEFAULT NULL COMMENT '最后更新人',
  `modifytime` datetime(6) DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`id`),
  KEY `ix_sys_user_deptid` (`deptid`),
  CONSTRAINT `fk_sys_user_sys_organization_deptid` FOREIGN KEY (`deptid`) REFERENCES `sys_organization` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='管理员';

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('1600000000000', '0', 'alpha2008', '', '2020-11-04 00:00:00.000000', '1600000001000', 'alpha2008@tom.com', '余小猫', 'F579B08404B5051D5FA4408B53E27664', '18898658888', '1600000000010,1606156061057', '2mh6e', '2', '1', '1600000000000', '2020-08-08 08:08:08.888888', '1600000000000', '2022-11-04 20:11:49.766338');
INSERT INTO `sys_user` VALUES ('1606291099001', '0', 'adncgo2', '', '2020-11-25 00:00:00.000000', '1606155393003', 'beta2009@tom.com', '余二猫', 'A9B7CDA2D9001025FC02C40AF6A80D4E', '18987656789', '1606156061057', '880qx', '2', '1', '1600000000000', '2020-11-25 15:58:20.255014', '1600000000000', '2021-02-08 23:41:01.034853');
INSERT INTO `sys_user` VALUES ('1606293242002', '0', 'adncgo3', '', '2020-11-25 00:00:00.000000', '1606155436004', 'beta2009@tom.com', '余三猫', 'B273093C82A8E58C4E6E9673A8062092', '18898737334', '1600000000010,1606156061057', 'p110y', '1', '1', '1600000000000', '2020-11-25 16:34:03.074970', '1600000000000', '2021-02-08 23:20:03.098985');

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
INSERT INTO `__efmigrationshistory` VALUES ('20221220080247_Init2022122001', '6.0.6');
