-- MySQL dump 10.13  Distrib 5.6.24, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: BooksDB
-- ------------------------------------------------------
-- Server version	5.6.27
CREATE DATABASE  IF NOT EXISTS `BooksDB` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `BooksDB`;

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Books`
--

DROP TABLE IF EXISTS `Books`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Books` (
  `id` mediumint(8) unsigned NOT NULL AUTO_INCREMENT,
  `Title` text,
  `Author` varchar(255) DEFAULT NULL,
  `PublishDate` varchar(255) DEFAULT NULL,
  `ISBN` varchar(36) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Books`
--

LOCK TABLES `Books` WRITE;
/*!40000 ALTER TABLE `Books` DISABLE KEYS */;
INSERT INTO `Books` VALUES (1,'Pride and Prejudice (Oxford World\'s Classics) ','Austen, Jane','2008/03/14','9780199535569'),(2,'150th Anniversary Edition Illustrated by Salvador DalÃ Alice`s Adverntures in Wonderland Format: Hardback','Dalí, Salvador','2015/09/29','9780691170022'),(3,'Dreamcatcher ','King, Stephen','2001/05/14','9780743211383'),(4,'In Cold Blood ','Truman Capote','1994/02/01','9780679745587'),(5,'Misery','KING, Stephen','1992/03/18','9780340390702'),(6,'The Tommyknockers','King, Stephen','1995/09/10','9780451156600'),(7,'Nightmares and Dreamscapes ','King, Stephen','1993/03/02','9780451180230'),(8,'Anti-Social Register','Hitchcock','1996/07/13','9780440102168'),(9,'Murder on the Orient Express','Agatha Christie','1983/10/15','9781148758794'),(10,'100 Years Of Solitude ','Marquez, Gabriel Garcia','1990/06/17','9781417735983');
/*!40000 ALTER TABLE `Books` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-10-12  9:51:51
