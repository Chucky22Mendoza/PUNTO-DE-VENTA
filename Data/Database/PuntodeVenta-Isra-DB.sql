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
	`precio` DOUBLE NOT NULL,
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
