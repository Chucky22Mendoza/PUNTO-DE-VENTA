DELIMITER $$
CREATE PROCEDURE venta_totales(IN fecha_hora TIMESTAMP)
BEGIN
   SELECT SUM(total)
   	FROM venta
   		WHERE estado = 1 AND fecha_creacion > fecha_hora;
END$$
DELIMITER

DELIMITER $$
CREATE PROCEDURE ganancias(IN fecha_hora TIMESTAMP)
BEGIN
	SELECT SUM((p.precio - p.precio_costo) * aux.cantidad) AS ganancia FROM (SELECT vp.id_producto, vp.cantidad, vp.total
   	FROM venta v INNER JOIN venta_producto vp ON vp.id_venta = v.id_venta
   		WHERE v.estado = 1 AND v.fecha_creacion >= fecha_hora) AS aux INNER JOIN producto p
   			ON p.codigo = aux.id_producto;
END$$
DELIMITER

DELIMITER $$
CREATE PROCEDURE ventas_por_producto(IN fecha_hora TIMESTAMP)
BEGIN
	SELECT p.id_producto, p.nombre, p.codigo, SUM(aux.cantidad * aux.total) AS total_producto
		FROM (SELECT vp.id_producto, vp.cantidad, vp.total
	   	FROM venta v INNER JOIN venta_producto vp ON vp.id_venta = v.id_venta
	   		WHERE v.estado = 1 AND v.fecha_creacion >= fecha_hora) AS aux INNER JOIN producto p
	   			ON p.codigo = aux.id_producto
	   				GROUP BY id_producto;
END$$
DELIMITER

DELIMITER $$
CREATE PROCEDURE ventas_por_departamento(IN fecha_hora TIMESTAMP)
BEGIN
	SELECT dep.idDepartamento, dep.nombre, aux2.total_departamento 
		FROM (SELECT p.departamento, SUM(aux.cantidad * aux.total) AS total_departamento
			FROM (SELECT vp.id_producto, vp.cantidad, vp.total
		   	FROM venta v INNER JOIN venta_producto vp ON vp.id_venta = v.id_venta
		   		WHERE v.estado = 1 AND v.fecha_creacion >= fecha_hora) AS aux INNER JOIN producto p
		   			ON p.codigo = aux.id_producto
		   				GROUP BY departamento) AS aux2 INNER JOIN departamentos dep 
		   					ON dep.idDepartamento = aux2.departamento;
END$$
DELIMITER