----------------------------------------------
-- Export file for user EXPEDIENTES         --
-- Created by ivans on 08/11/2021, 1:22:45  --
----------------------------------------------



--prompt 
--prompt  Creating package PCK_EXPEDIENTES
--prompt  ================================
--prompt 
create or replace package expedientes.PCK_EXPEDIENTES as

  PROCEDURE USP_EXPEDIENTE_PAGINACION
  (
    PI_PAGINA               IN INTEGER,
    PI_NROREGISTROS         IN INTEGER,
    PI_ORDEN_COLUMNA        IN VARCHAR2,
    PI_ORDEN                IN VARCHAR2,
    PI_WHERE                VARCHAR2 DEFAULT NULL,
    PO_CUENTA               OUT INT,
    PO_RESULTADO            OUT SYS_REFCURSOR
  ); 

end PCK_EXPEDIENTES;
/

--prompt 
--prompt  Creating package body PCK_EXPEDIENTES
--prompt  =====================================
--prompt 
create or replace package body expedientes.PCK_EXPEDIENTES as

  PROCEDURE USP_EXPEDIENTE_PAGINACION
  (
    PI_PAGINA               IN INTEGER,
    PI_NROREGISTROS         IN INTEGER,
    PI_ORDEN_COLUMNA        IN VARCHAR2,
    PI_ORDEN                IN VARCHAR2,
    PI_WHERE                VARCHAR2 DEFAULT NULL,
    PO_CUENTA               OUT INT,
    PO_RESULTADO            OUT SYS_REFCURSOR
  )
  IS
    P_PAGINA_AUX              INTEGER := 0;
    P_CUENTA_REGISTROS        INTEGER := 0;
    P_CUENTA_PAGINA           INTEGER := 0;
    P_NROREGISTROS_AUX        INTEGER := PI_NROREGISTROS;
    P_ORDEN_COLUMNA_AUX       VARCHAR2(100);
    P_SQLDR                   VARCHAR2(10000);
  BEGIN
      --PO_CUENTA := 0;
      P_ORDEN_COLUMNA_AUX := PI_ORDEN_COLUMNA;
      IF P_NROREGISTROS_AUX = 0 THEN
          P_NROREGISTROS_AUX := 1;
      END IF;
      IF P_ORDEN_COLUMNA_AUX IS NULL THEN
          P_ORDEN_COLUMNA_AUX := 'ID_EXPEDIENTE';
      END IF;
      P_SQLDR := 'SELECT
                      COUNT(*)
                  FROM
                      V_EXPEDIENTES V WHERE ' || PI_WHERE;
      --INSERT INTO LOGG VALUES('P_PAGINA_AUX:'||P_SQLDR);
      EXECUTE IMMEDIATE P_SQLDR INTO P_CUENTA_REGISTROS;
      -- Paginas totales
      P_CUENTA_PAGINA := CEIL(TO_NUMBER(P_CUENTA_REGISTROS) / TO_NUMBER(PI_PAGINA));

      IF PI_PAGINA > P_CUENTA_PAGINA THEN
          P_PAGINA_AUX := P_CUENTA_PAGINA - 1;
      ELSE
          P_PAGINA_AUX := PI_PAGINA - 1 ;
      END IF;
      --INSERT INTO LOGG VALUES('P_NROREGISTROS_AUX:'||P_NROREGISTROS_AUX);
      --INSERT INTO LOGG VALUES('P_PAGINA_AUX:'||P_PAGINA_AUX);
      P_SQLDR := '
                 SELECT
                     V.*,
                     ROWNUM FILA,
                     ROW_NUMBER() OVER (ORDER BY ' || P_ORDEN_COLUMNA_AUX || ' ' || PI_ORDEN || ') AS ROWNUMBER
                 FROM V_EXPEDIENTES V';
      IF PI_WHERE <> ' ' AND PI_WHERE IS NOT NULL THEN
          P_SQLDR := P_SQLDR || ' WHERE ' || PI_WHERE;
      END IF;
      P_SQLDR := 'SELECT * FROM (' || P_SQLDR || ') WHERE ROWNUMBER BETWEEN ' ||
      TO_CHAR((P_PAGINA_AUX * P_NROREGISTROS_AUX) + 1) || ' AND ' || TO_CHAR(P_NROREGISTROS_AUX  *( P_PAGINA_AUX + 1)) ||
      ' ORDER BY ' || P_ORDEN_COLUMNA_AUX || ' ' || PI_ORDEN;
      PO_CUENTA := P_CUENTA_REGISTROS;
     -- INSERT INTO LOGG VALUES(P_SQLDR);
      --COMMIT;
      OPEN PO_RESULTADO FOR P_SQLDR;
  END;
  
end PCK_EXPEDIENTES;
/



