----------------------------------------------
-- Export file for user EXPEDIENTES         --
-- Created by ivans on 15/11/2021, 2:26:32  --
----------------------------------------------



--prompt
--prompt Creating view V_ADJUNTOS
--prompt ========================
--prompt
CREATE OR REPLACE VIEW EXPEDIENTES.V_ADJUNTOS AS
SELECT
      V.ID_ADJUNTO,
      V.ID_MAESTRA,
      V.ID_SISTEMA,
      V.ID_TIPO_ARCHIVO,
      D.DESC_CORTA_DOMINIO DESC_ARCHIVO,
      V.NUMERO,
      TO_CHAR(V.FECHA,'DD/MM/YYYY ') FECHA,
      V.OBSERVACION,
      V.NOMBRE_ARCHIVO,
      V.EXTENSION_ARCHIVO,
      V.FLG_ESTADO,
      V.USU_CREACION,
      TO_CHAR(V.FEC_CREACION,'DD/MM/YYYY ') FEC_CREACION,
      V.USU_MODIFICACION,
      TO_CHAR(V.FEC_MODIFICACION,'DD/MM/YYYY ') FEC_MODIFICACION

  FROM
     T_EXPD_ADJUNTOS V
     INNER JOIN T_EXPM_DOMINIO D ON D.ID_DOMINIO = V.ID_TIPO_ARCHIVO
/

--prompt
--prompt Creating view V_OFICINA
--prompt =======================
--prompt
CREATE OR REPLACE VIEW EXPEDIENTES.V_OFICINA AS
SELECT
      ID_OFICINA,
      DESC_OFICINA,
      DESC_CORTA,
      NVL(ACRONIMO,'') ACRONIMO
  FROM
     BITACORA.t_bitl_oficina
  WHERE
      ID_TIPO_OFICINA = '03'
/

--prompt
--prompt Creating view V_PERSONAL
--prompt ========================
--prompt
CREATE OR REPLACE VIEW EXPEDIENTES.V_PERSONAL AS
SELECT
       DISTINCT(P.ID_PERSONAL) ID_PERSONAL,
       CONCAT(CONCAT(P.APELLIDO_PATERNO || ' ' ,P.APELLIDO_MATERNO || ' '),P.NOMBRES)  NOMBRE_COMPLETO,
       SITLAB.DESC_SITUACION_LABORAL  REGIMEN_LABORAL,
       OFI_.DESC_CORTA OFICINA,
       DNI.NUMERO_DNI DNI,
       PU.DESC_PUESTO


    FROM
       BITACORA.T_BITM_PERSONAL P
       LEFT JOIN BITACORA.T_BITD_PERSONAL_INCORPORACION SITL ON SITL.ID_PERSONAL = P.ID_PERSONAL AND SITL.FLG_ESTADO=1
       LEFT JOIN BITACORA.T_BITL_SITUACION_LABORAL SITLAB ON SITLAB.ID_SITUACION_LABORAL = SITL.ID_REGIMEN_LABORAL
       LEFT JOIN BITACORA.T_BITJ_PERSONAL_OFICINA OFI ON OFI.ID_PERSONAL = P.ID_PERSONAL AND OFI.FLG_ESTADO = 1
       LEFT JOIN BITACORA.T_BITL_OFICINA OFI_ ON OFI_.ID_OFICINA = OFI.ID_OFICINA
       LEFT JOIN BITACORA.T_BITD_PERSONAL_DNI DNI ON DNI.ID_PERSONAL = P.ID_PERSONAL AND DNI.FLG_ESTADO=1 AND DNI.TIPO_DNI ='00001'
       LEFT JOIN BITACORA.T_BITD_PERSONAL_INCORPORACION INC ON INC.ID_PERSONAL = P.ID_PERSONAL AND INC.FLG_ESTADO=1
       LEFT JOIN BITACORA.V_PUESTO PU ON PU.ID_PUESTO = INC.ID_PUESTO

     WHERE P.FLG_ESTADO = 1
/

--prompt
--prompt Creating view V_EXPEDIENTES
--prompt ===========================
--prompt
CREATE OR REPLACE VIEW EXPEDIENTES.V_EXPEDIENTES AS
SELECT
      V.ID_EXPEDIENTE,
      V.ID_PERSONAL,
      P.NOMBRE_COMPLETO,
      P.REGIMEN_LABORAL,
      P.OFICINA,
      P.DESC_PUESTO,
      P.DNI,
      V.COD_EXPEDIENTE,
      TO_CHAR(V.FECHA_RECEPCION,'DD/MM/YYYY') FECHA_RECEPCION,
      TO_CHAR(V.FECHA_PRESCRIPCION,'DD/MM/YYYY ') FECHA_PRESCRIPCION,
      TO_CHAR(V.FECHA_HECHO,'DD/MM/YYYY') FECHA_HECHO,
      V.HOJA_RUTA,
      V.ID_REMITENTE,
      RE.DESC_OFICINA REMITENTE,
      NVL(V.ID_ACTO,0) ID_ACTO,
      AC.DESC_CORTA_DOMINIO ACTO,
      V.OBSERVACION_INVESTIGADORA,
      NVL(V.ID_FALTA,0) ID_FALTA,
      FA.DESC_CORTA_DOMINIO FALTA,
      V.ARTICULO,
      V.INC,
      V.OBSERVACION_INSTRUCTORA,
      NVL(V.ID_PRECALIFICACION,0) ID_PRECALIFICACION,
      V.TIPO_SANCION_RECOMENDADA,
      V.ACTO_INICIO,
      TO_CHAR(V.FECHA_NOTIFICACION,'DD/MM/YYYY ') FECHA_NOTIFICACION,
      V.RECOMENDACION_PREINFORME,
      NVL(V.ID_SANCION_RECOMENDADA,0) ID_SANCION_RECOMENDADA,
      SA.DESC_CORTA_DOMINIO SANCION_RECOMENDADA,
      V.ID_ORGANO_INSTRUCTOR,
      OI.DESC_OFICINA ORGANO_INSTRUCTOR,
      TO_CHAR(V.FECHA_NOTIFICACION_INICIO,'DD/MM/YYYY') FECHA_NOTIFICACION_INICIO,
      V.DOCUMENTO_FINALIZACION,
      V.RECOMENDACION_INSTRUCTOR,
      V.ID_ORGANO_SANCIONADOR,
      OS.DESC_OFICINA ORGANO_SANCIONADOR,
      V.SANCION,
      V.OBSERVACION_SANCIONADORA,
      NVL(V.ID_SITUACION,0) ID_SITUACION,
      SIT.DESC_CORTA_DOMINIO SITUACION,
      NVL(V.ID_ESTADO,0) ID_ESTADO,
      ES.DESC_CORTA_DOMINIO ESTADO,
      V.DOCUMENTO_NOTIFICA,
      V.DIAS_VIGENTE,
      V.EXTENSION_ARCHIVO,
      V.FLG_ESTADO,
      TO_CHAR(V.FEC_CREACION,'DD/MM/YYYY hh:mi:ss a.m.') FEC_CREACION,
      TO_CHAR(V.FEC_MODIFICACION,'DD/MM/YYYY hh:mi:ss a.m.') FEC_MODIFICACION,
      V.USU_CREACION,
      V.USU_MODIFICACION,
      V.IP_MODIFICACION,
      V.IP_CREACION,
      TO_DATE(SYSDATE,'DD/MM/YYYY') - TO_DATE(V.FECHA_PRESCRIPCION,'DD/MM/YYYY') as DIAS

  FROM
     T_EXPM_EXPEDIENTES V
     LEFT JOIN V_PERSONAL P ON P.ID_PERSONAL = V.ID_PERSONAL
     LEFT JOIN T_EXPM_DOMINIO AC ON AC.ID_DOMINIO = V.ID_ACTO
     LEFT JOIN T_EXPM_DOMINIO ES ON ES.ID_DOMINIO = V.ID_ESTADO
     LEFT JOIN T_EXPM_DOMINIO SIT ON SIT.ID_DOMINIO = V.ID_SITUACION
     LEFT JOIN T_EXPM_DOMINIO SA ON SA.ID_DOMINIO = V.ID_SANCION_RECOMENDADA
     LEFT JOIN T_EXPM_DOMINIO FA ON FA.ID_DOMINIO = V.ID_FALTA
     LEFT JOIN V_OFICINA OI ON OI.ID_OFICINA = V.ID_ORGANO_INSTRUCTOR
     LEFT JOIN V_OFICINA OS ON OS.ID_OFICINA = V.ID_ORGANO_SANCIONADOR
     LEFT JOIN V_OFICINA RE ON RE.ID_OFICINA = V.ID_REMITENTE
/



