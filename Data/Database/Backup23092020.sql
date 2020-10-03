-- --------------------------------------------------------
-- Host:                         35.236.8.43
-- Versión del servidor:         8.0.18-google - (Google)
-- SO del servidor:              Linux
-- HeidiSQL Versión:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Volcando estructura de base de datos para db_punto_venta
CREATE DATABASE IF NOT EXISTS `db_punto_venta` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_punto_venta`;

-- Volcando estructura para tabla db_punto_venta.corte_caja
CREATE TABLE IF NOT EXISTS `corte_caja` (
  `id_corte_caja` int(11) NOT NULL AUTO_INCREMENT,
  `dinero_inicial` int(11) NOT NULL,
  `fecha_corte_inicio` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fecha_corte_fin` timestamp NULL DEFAULT NULL,
  `ganancia` double DEFAULT NULL,
  `ventas_totales` double DEFAULT NULL,
  PRIMARY KEY (`id_corte_caja`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Volcando datos para la tabla db_punto_venta.corte_caja: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `corte_caja` DISABLE KEYS */;
/*!40000 ALTER TABLE `corte_caja` ENABLE KEYS */;

-- Volcando estructura para tabla db_punto_venta.producto
CREATE TABLE IF NOT EXISTS `producto` (
  `id_producto` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `descripcion` varchar(1000) DEFAULT NULL,
  `codigo_barras` varchar(255) DEFAULT NULL,
  `codigo` varchar(255) NOT NULL,
  `precio_venta` double(22,0) NOT NULL,
  `existencia` int(11) NOT NULL,
  `fecha_registro` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fecha_actualizacion` timestamp NULL DEFAULT NULL,
  `precio_costo` double(22,0) DEFAULT NULL,
  `precio_mayoreo` double(22,0) DEFAULT NULL,
  `min_inventario` int(11) DEFAULT NULL,
  `tipo_venta` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_producto`),
  UNIQUE KEY `un_codigo` (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Volcando datos para la tabla db_punto_venta.producto: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `producto` DISABLE KEYS */;
INSERT IGNORE INTO `producto` (`id_producto`, `nombre`, `descripcion`, `codigo_barras`, `codigo`, `precio_venta`, `existencia`, `fecha_registro`, `fecha_actualizacion`, `precio_costo`, `precio_mayoreo`, `min_inventario`, `tipo_venta`) VALUES
	(1, 'Test0', 'Some descripcion', '123123990', 'pruebacodigo0', 23, 9, '2020-09-20 21:19:01', NULL, 20, 21, 5, 1),
	(2, 'Test1', 'Some descripcion', '123123991', 'pruebacodigo1', 11, 15, '2020-09-20 22:07:31', NULL, 8, 9, 7, 1),
	(3, 'Test2', 'Some descripcion', '123123992', 'pruebacodigo2', 22, 25, '2020-09-20 21:28:44', NULL, 21, 22, 3, 2),
	(4, 'Test3', 'Some descripcion', '123123993', 'pruebacodigo3', 9, 5, '2020-09-20 22:17:15', NULL, 6, 7, 8, 1),
	(5, 'Test4', 'Some descripcion', '123123994', 'pruebacodigo4', 2, 6, '2020-09-20 21:28:44', NULL, 0, 0, 8, 1),
	(6, 'Test5', 'Some descripcion', '123123995', 'pruebacodigo5', 156, 66, '2020-09-20 21:28:44', NULL, 120, 100, 21, 2),
	(7, 'Test6', 'Some descripcion', '123123996', 'pruebacodigo6', 44, 20, '2020-09-20 22:06:17', NULL, 42, 40, 5, 3),
	(8, 'Test7', 'Some descripcion', '123123997', 'pruebacodigo7', 54, 4, '2020-09-20 21:28:44', NULL, 40, 35, 9, 2),
	(9, 'Test8', 'Some descripcion', '123123998', 'pruebacodigo8', 5, 22, '2020-09-20 22:19:31', NULL, 3, 3, 10, 1),
	(10, 'Test9', 'Some descripcion', '123123999', 'pruebacodigo9', 12, 60, '2020-09-20 21:28:44', NULL, 12, 10, 12, 1);
/*!40000 ALTER TABLE `producto` ENABLE KEYS */;

-- Volcando estructura para tabla db_punto_venta.venta
CREATE TABLE IF NOT EXISTS `venta` (
  `id_venta` int(11) NOT NULL AUTO_INCREMENT,
  `total` double NOT NULL,
  `estado` int(11) NOT NULL,
  `total_articulos` int(11) NOT NULL,
  `fecha_registro` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_venta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
  `fecha_registro` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_venta_producto`),
  KEY `venta_producto_id_venta-venta_id_venta` (`id_venta`),
  KEY `venta_producto_id_producto-producto_id_producto` (`id_producto`),
  CONSTRAINT `venta_producto_id_producto-producto_id_producto` FOREIGN KEY (`id_producto`) REFERENCES `producto` (`id_producto`) ON UPDATE CASCADE,
  CONSTRAINT `venta_producto_id_venta-venta_id_venta` FOREIGN KEY (`id_venta`) REFERENCES `venta` (`id_venta`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Volcando datos para la tabla db_punto_venta.venta_producto: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `venta_producto` DISABLE KEYS */;
/*!40000 ALTER TABLE `venta_producto` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
