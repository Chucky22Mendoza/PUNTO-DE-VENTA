USE db_punto_venta;

set global sql_mode = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

DROP TABLE IF EXISTS `venta_producto`;
DROP TABLE IF EXISTS `producto`;
DROP TABLE IF EXISTS `venta`;

CREATE TABLE `producto` (
	`id_producto` INT AUTO_INCREMENT NOT NULL,
	`nombre` VARCHAR (255) NOT NULL,
	`descripcion` VARCHAR (1000),
	`codigo_barras` VARCHAR (255),
	`codigo` VARCHAR (255) NOT NULL,
	`precio_venta` DOUBLE NOT NULL,
	`existencia` INT NOT NULL,
	`fecha_creacion` TIMESTAMP NOT NULL,
	`fecha_actualizacion` TIMESTAMP NOT NULL,
    	CONSTRAINT `un_codigo` UNIQUE (`codigo`),
	PRIMARY KEY (`id_producto`)
);

CREATE TABLE `venta` (
	`id_venta` INT AUTO_INCREMENT NOT NULL,
	`total` DOUBLE NOT NULL,
	`estado` INT NOT NULL,
	`total_articulos` INT NOT NULL,
	`fecha_creacion` TIMESTAMP NOT NULL,
	PRIMARY KEY (`id_venta`)
);

CREATE TABLE `venta_producto` (
	`id_venta_producto` INT AUTO_INCREMENT NOT NULL,
	`id_venta` INT NOT NULL,
	`id_producto` INT NOT NULL,
	`cantidad` INT NOT NULL,
	`total` DOUBLE NOT NULL,
	CONSTRAINT `venta_producto_id_venta-venta_id_venta` FOREIGN KEY (`id_venta`) REFERENCES `venta`(`id_venta`) ON UPDATE CASCADE ON DELETE NO ACTION,
	CONSTRAINT `venta_producto_id_producto-producto_id_producto` FOREIGN KEY (`id_producto`) REFERENCES `producto`(`id_producto`) ON UPDATE CASCADE ON DELETE NO ACTION,
	PRIMARY KEY (`id_venta_producto`)
);

CREATE TABLE `corte_caja` (
	`id_corte_caja` INT AUTO_INCREMENT NOT NULL,
	`dinero_inicial` INT NOT NULL,
	`fecha_corte_inicio` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
	`fecha_corte_fin` TIMESTAMP NULL,
	`ganancia` DOUBLE,
	`ventas_totales` DOUBLE,
	PRIMARY KEY (`id_corte_caja`)
);

ALTER TABLE producto ADD COLUMN `fecha_registro` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP();
ALTER TABLE producto ADD COLUMN `precio_costo` DOUBLE;
ALTER TABLE producto ADD COLUMN `precio_mayoreo` DOUBLE;
ALTER TABLE producto ADD COLUMN `min_inventario` INT;
ALTER TABLE producto ADD COLUMN `tipo_venta` INT;

INSERT INTO `producto` (`id_producto`, `nombre`, `descripcion`, `codigo_barras`, `codigo`, `precio_venta`, `existencia`, `fecha_registro`, `fecha_actualizacion`, `precio_costo`, `precio_mayoreo`, `min_inventario`, `tipo_venta`) VALUES
	(1, 'Test0', 'Some descripcion', '123123990', 'pruebacodigo0', 23, 9, '2020-09-20 21:19:01', NULL, 20, 21, 5, 1),
	(2, 'Test1', 'Some descripcion', '123123991', 'pruebacodigo1', 11, 15, '2020-09-20 22:07:31', NULL, 8, 9, 7, 1),
	(3, 'Test2', 'Some descripcion', '123123992', 'pruebacodigo2', 22.3, 25, '2020-09-20 21:28:44', NULL, 21, 22, 3, 2),
	(4, 'Test3', 'Some descripcion', '123123993', 'pruebacodigo3', 8.8, 5, '2020-09-20 22:17:15', NULL, 6, 7, 8, 1),
	(5, 'Test4', 'Some descripcion', '123123994', 'pruebacodigo4', 2, 6, '2020-09-20 21:28:44', NULL, 0.5, 0.25, 8, 1),
	(6, 'Test5', 'Some descripcion', '123123995', 'pruebacodigo5', 156, 66, '2020-09-20 21:28:44', NULL, 120, 100, 21, 2),
	(7, 'Test6', 'Some descripcion', '123123996', 'pruebacodigo6', 44.5, 20, '2020-09-20 22:06:17', NULL, 42.5, 40, 5, 3),
	(8, 'Test7', 'Some descripcion', '123123997', 'pruebacodigo7', 53.99, 4, '2020-09-20 21:28:44', NULL, 40, 35, 9, 2),
	(9, 'Test8', 'Some descripcion', '123123998', 'pruebacodigo8', 5, 22, '2020-09-20 22:19:31', NULL, 3.5, 3, 10, 1),
	(10, 'Test9', 'Some descripcion', '123123999', 'pruebacodigo9', 12.5, 60, '2020-09-20 21:28:44', NULL, 12, 10, 12, 1);
