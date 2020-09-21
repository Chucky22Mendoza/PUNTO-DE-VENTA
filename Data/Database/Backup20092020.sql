-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.4.13-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Volcando estructura de base de datos para db_punto_venta
CREATE DATABASE IF NOT EXISTS `db_punto_venta` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `db_punto_venta`;

-- Volcando estructura para tabla db_punto_venta.producto
CREATE TABLE IF NOT EXISTS `producto` (
  `id_producto` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `descripcion` varchar(1000) NOT NULL,
  `codigo_barras` varchar(255) DEFAULT NULL,
  `codigo` varchar(255) NOT NULL,
  `precio` double NOT NULL,
  `existencia` int(11) NOT NULL,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `fecha_actualizacion` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id_producto`),
  UNIQUE KEY `un_codigo` (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla db_punto_venta.producto: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `producto` DISABLE KEYS */;
INSERT IGNORE INTO `producto` (`id_producto`, `nombre`, `descripcion`, `codigo_barras`, `codigo`, `precio`, `existencia`, `fecha_creacion`, `fecha_actualizacion`) VALUES
	(1, 'Test', 'Some descripcion', '123123999', 'pruebacodigo', 23, 9, '2020-09-20 21:19:01', NULL),
	(2, 'Test2', 'Some descripcion', '123123990', 'pruebacodigo2', 11, 15, '2020-09-20 22:07:31', NULL),
	(3, 'Test3', 'Some descripcion', '123123991', 'pruebacodigo3', 22.3, 25, '2020-09-20 21:28:44', NULL),
	(4, 'Test4', 'Some descripcion', '123123992', 'pruebacodigo4', 8.8, 5, '2020-09-20 22:17:15', NULL),
	(5, 'Test5', 'Some descripcion', '123123993', 'pruebacodigo5', 2, 6, '2020-09-20 21:28:44', NULL),
	(6, 'Test6', 'Some descripcion', '123123994', 'pruebacodigo6', 156, 66, '2020-09-20 21:28:44', NULL),
	(7, 'Test7', 'Some descripcion', '123123995', 'pruebacodigo7', 44.5, 20, '2020-09-20 22:06:17', NULL),
	(8, 'Test8', 'Some descripcion', '123123996', 'pruebacodigo8', 53.99, 4, '2020-09-20 21:28:44', NULL),
	(9, 'Test9', 'Some descripcion', '123123997', 'pruebacodigo9', 5, 22, '2020-09-20 22:19:31', NULL),
	(10, 'Test0', 'Some descripcion', '123123998', 'pruebacodigo0', 12.5, 60, '2020-09-20 21:28:44', NULL);
/*!40000 ALTER TABLE `producto` ENABLE KEYS */;

-- Volcando estructura para tabla db_punto_venta.venta
CREATE TABLE IF NOT EXISTS `venta` (
  `id_venta` int(11) NOT NULL AUTO_INCREMENT,
  `total` double NOT NULL,
  `estado` int(11) NOT NULL,
  `total_articulos` int(11) NOT NULL,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_venta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla db_punto_venta.venta: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `venta` DISABLE KEYS */;
/*!40000 ALTER TABLE `venta` ENABLE KEYS */;

-- Volcando estructura para tabla db_punto_venta.venta_producto
CREATE TABLE IF NOT EXISTS `venta_producto` (
  `id_venta_producto` int(11) NOT NULL AUTO_INCREMENT,
  `id_venta` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `total` double NOT NULL,
  PRIMARY KEY (`id_venta_producto`),
  KEY `venta_producto_id_venta-venta_id_venta` (`id_venta`),
  KEY `venta_producto_id_producto-producto_id_producto` (`id_producto`),
  CONSTRAINT `venta_producto_id_producto-producto_id_producto` FOREIGN KEY (`id_producto`) REFERENCES `producto` (`id_producto`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `venta_producto_id_venta-venta_id_venta` FOREIGN KEY (`id_venta`) REFERENCES `venta` (`id_venta`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla db_punto_venta.venta_producto: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `venta_producto` DISABLE KEYS */;
/*!40000 ALTER TABLE `venta_producto` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
