CREATE TABLE `corte_caja` (
  `id_corte_caja` int(11) NOT NULL AUTO_INCREMENT,
  `dinero_inicial` int(11) NOT NULL,
  `fecha_corte_inicio` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_corte_fin` timestamp NULL DEFAULT current_timestamp(),
  `ganancia` double DEFAULT NULL,
  `ventas_totales` double DEFAULT NULL,
  PRIMARY KEY (`id_corte_caja`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `departamentos` (
  `idDepartamento` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idDepartamento`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `entradas_salidas` (
  `identradas_salidas` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` int(11) DEFAULT NULL,
  `cantidad` double DEFAULT NULL,
  `date_create` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`identradas_salidas`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `tempventa` (
  `idTV` int(11) NOT NULL AUTO_INCREMENT,
  `codigo` varchar(45) DEFAULT NULL,
  `cantidad` double DEFAULT NULL,
  `total` double DEFAULT NULL,
  PRIMARY KEY (`idTV`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `tipoventa` (
  `idTipo` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` varchar(45) NOT NULL,
  PRIMARY KEY (`idTipo`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `venta` (
  `id_venta` int(11) NOT NULL AUTO_INCREMENT,
  `total` double NOT NULL,
  `estado` int(11) NOT NULL,
  `total_articulos` double NOT NULL,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id_venta`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `producto` (
  `id_producto` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `descripcion` varchar(1000) DEFAULT NULL,
  `codigo` varchar(255) NOT NULL,
  `precio` double NOT NULL,
  `existencia` int(11) NOT NULL,
  `fecha_creacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_actualizacion` timestamp NOT NULL DEFAULT current_timestamp(),
  `fecha_registro` timestamp NOT NULL DEFAULT current_timestamp(),
  `precio_costo` double DEFAULT NULL,
  `precio_mayoreo` double DEFAULT NULL,
  `min_inventario` int(11) DEFAULT NULL,
  `tipo_venta` int(11) DEFAULT NULL,
  `departamento` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_producto`),
  UNIQUE KEY `un_codigo` (`codigo`),
  KEY `FK_departamento` (`departamento`),
  KEY `FK_tipoVenta` (`tipo_venta`),
  CONSTRAINT `FK_departamento` FOREIGN KEY (`departamento`) REFERENCES `departamentos` (`idDepartamento`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_tipoVenta` FOREIGN KEY (`tipo_venta`) REFERENCES `tipoventa` (`idTipo`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `venta_producto` (
  `id_venta_producto` int(11) NOT NULL AUTO_INCREMENT,
  `id_venta` int(11) NOT NULL,
  `id_producto` varchar(255) NOT NULL,
  `cantidad` double NOT NULL,
  `total` double NOT NULL,
  PRIMARY KEY (`id_venta_producto`),
  KEY `venta_producto_id_venta-venta_id_venta` (`id_venta`),
  KEY `venta_producto_id_producto-producto_id_producto` (`id_producto`),
  CONSTRAINT `venta_producto_id_venta-venta_id_venta` FOREIGN KEY (`id_venta`) REFERENCES `venta` (`id_venta`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb4;
