/*
Navicat MySQL Data Transfer

Source Server         : bcpa user
Source Server Version : 100125
Source Host           : localhost:3306
Source Database       : bcpaots

Target Server Type    : MYSQL
Target Server Version : 100125
File Encoding         : 65001

Date: 2017-12-03 02:08:25
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for userdata
-- ----------------------------
DROP TABLE IF EXISTS `userdata`;
CREATE TABLE `userdata` (
  `UserID` int(255) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `UserType` varchar(255) NOT NULL DEFAULT 'Customer',
  `EmailAddress` varchar(255) DEFAULT NULL,
  `PasswordHash` varchar(255) DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `MiddleName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `Country` varchar(255) DEFAULT NULL,
  `County` varchar(255) DEFAULT NULL,
  `HouseNumber` varchar(255) DEFAULT NULL,
  `AddressLine1` varchar(255) DEFAULT NULL,
  `AddressLine2` varchar(255) DEFAULT NULL,
  `AddressLine3` varchar(255) DEFAULT NULL,
  `TownCity` varchar(255) DEFAULT NULL,
  `PostCode` varchar(255) DEFAULT NULL,
  `DateOfBirth` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of userdata
-- ----------------------------
INSERT INTO `userdata` VALUES ('000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004', 'Customer', 'FirstNameMiddleNameLastName@MailServerName.co.uk', 'test', 'Mr.', 'FirstName', 'MiddleName', 'LastName', 'United Kingdom', 'Bucks', 'houseNumName', 'addr1', 'addr2', 'addr3', 'town name', 'ALPO ST00', '15/6/1980');
