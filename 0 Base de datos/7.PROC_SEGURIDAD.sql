----------------------------------------------
-- Export file for user SEGURIDAD           --
-- Created by ivans on 15/11/2021, 2:32:22  --
----------------------------------------------



--prompt
--prompt Creating package PCK_SEEGURIDAD_LDAP
--prompt ====================================
--prompt
create or replace package seguridad.PCK_SEEGURIDAD_LDAP is

  -- Author  : PERCY LICITO
  -- Created : 27/08/2021 17:41:57
  -- Purpose : PCK_SEEGURIDAD_LDAP

 PROCEDURE USP_USUARIO_DETALLE
(

  PI_ID_SISTEMA NUMBER,
  PI_USUARIO   VARCHAR2,
  PI_Results        OUT sys_refcursor
);
PROCEDURE USP_MENU_USUARIO
(

  PI_ID_USUARIO NUMBER,
  PI_ID_SISTEMA NUMBER,
  PI_Results  OUT sys_refcursor
);

PROCEDURE USP_USUARIO_DET_PERFIL_LISTAR
(

  PI_ID_SISTEMA NUMBER,
  PI_USUARIO   VARCHAR2,
  PI_Results  OUT sys_refcursor
); 

PROCEDURE USP_MENU_USUARIO_PERFIL_LISTAR
(

  PI_ID_USUARIO NUMBER,
  PI_ID_SISTEMA NUMBER,
  PI_ID_PERFIL NUMBER, 
  PI_Results  OUT  sys_refcursor
);
end ;
/

--prompt
--prompt Creating package body PCK_SEEGURIDAD_LDAP
--prompt =========================================
--prompt
create or replace package body seguridad.PCK_SEEGURIDAD_LDAP is

PROCEDURE USP_USUARIO_DETALLE
(

  PI_ID_SISTEMA NUMBER,
  PI_USUARIO   VARCHAR2,
  PI_Results  OUT sys_refcursor
)
IS
BEGIN
     OPEN PI_Results FOR
                SELECT
                    USU.ID_USUARIO,
                    UPPER(TRIM(USU.COD_USUARIO)) AS LOGIN_USUARIO,
                    USU.ID_PERSONA,
                    NVL(OFI.ID_OFICINA,0) ID_OFICINA,
                    UPPER(OFI.DESC_OFICINA) AS NOMBRE_OFICINA,
                    UPPER(OFI.ACRONIMO) AS SIGLA,
                    UPPER(TRIM(PER.NOMBRES) || ' ' ||  TRIM(PER.APELLIDO_PATERNO) || ' ' || TRIM(PER.APELLIDO_MATERNO)) NOMBRE_PERSONA
                FROM
                    SEGURIDAD.T_SEGM_USUARIO USU
                    INNER JOIN SEGURIDAD.T_SEGJ_SISTEMAS_PERFIL_USUARIO SPU ON TRIM(SPU.ID_USUARIO) = TRIM(USU.ID_USUARIO)
                    INNER JOIN BITACORA.T_BITM_PERSONAL PER ON TRIM(PER.ID_PERSONAL) = TRIM(USU.ID_PERSONA)
                    LEFT JOIN BITACORA.T_BITJ_PERSONAL_OFICINA PO ON TRIM(PO.ID_PERSONAL) = TRIM(PER.ID_PERSONAL)
                    LEFT JOIN BITACORA.T_BITL_OFICINA OFI ON OFI.ID_OFICINA = PO.ID_OFICINA
                WHERE
                    UPPER(TRIM(USU.COD_USUARIO)) = UPPER(TRIM(PI_USUARIO))
                    AND SPU.ID_SISTEMA = PI_ID_SISTEMA
                    AND USU.FLG_ESTADO='1'
                    ;
END;

PROCEDURE USP_MENU_USUARIO
(

  PI_ID_USUARIO NUMBER,
  PI_ID_SISTEMA NUMBER,
  PI_Results  OUT  sys_refcursor
)
AS
BEGIN
  OPEN PI_Results FOR
    SELECT
        SM.ID_SISTEMA_MODULO,
        SM.ID_SISTEMA_MODULO_PADRE,
        SM.ID_TIPO_MODULO,
        SM.ID_A,
        SM.ID_LI,
        SM.DESC_MODULO,
        SM.IMAGEN,
        SM.ORDEN,
        SM.URL_MODULO
    FROM
        SEGURIDAD.T_SEGJ_SISTEMAS_PERFIL_USUARIO SPU
        INNER JOIN SEGURIDAD.T_SEGJ_SISTEMAS_PERFIL_MODULO SPM ON SPM.ID_PERFIL = SPU.ID_PERFIL
                                                                  AND SPM.ID_SISTEMA = SPU.ID_SISTEMA
                                                                  AND SPM.FLG_ESTADO = 1
        INNER JOIN SEGURIDAD.T_SEGD_SISTEMAS_MODULO SM ON SM.ID_SISTEMA_MODULO = SPM.ID_MODULO
                                                          AND SM.FLG_ESTADO = 1
   WHERE
        SPU.ID_SISTEMA = PI_ID_SISTEMA
        AND SPU.ID_USUARIO = PI_ID_USUARIO
       --- AND SPU.ID_PERFIL = PI_ID_PERFIL
        AND SPU.Flg_Estado = '1'
   ORDER BY
        SM.ORDEN ASC;

END;


PROCEDURE USP_USUARIO_DET_PERFIL_LISTAR
(

  PI_ID_SISTEMA NUMBER,
  PI_USUARIO   VARCHAR2,
  PI_Results  OUT sys_refcursor
)
IS
BEGIN
     OPEN PI_Results FOR
                SELECT
                    SP.ID_SISTEMA_PERFIL ID_PERFIL, 
                    SP.DESC_PERFIL,
                    USU.ID_USUARIO,
                    UPPER(TRIM(USU.COD_USUARIO)) AS LOGIN_USUARIO,
                    USU.ID_PERSONA,
                    NVL(OFI.ID_OFICINA,0) ID_OFICINA,
                    CASE WHEN LENGTH(OFI.DESC_OFICINA) > 50 THEN
                        SUBSTR(OFI.DESC_OFICINA,0,50)
                    ELSE
                        OFI.DESC_OFICINA
                    END   NOMBRE_OFICINA,
                    UPPER(OFI.ACRONIMO) AS SIGLA,
                    UPPER(TRIM(PER.NOMBRES) || ' ' ||  TRIM(PER.APELLIDO_PATERNO) || ' ' || TRIM(PER.APELLIDO_MATERNO)) NOMBRE_PERSONA
                FROM
                    SEGURIDAD.T_SEGM_USUARIO USU
                    INNER JOIN SEGURIDAD.T_SEGJ_SISTEMAS_PERFIL_USUARIO SPU ON TRIM(SPU.ID_USUARIO) = TRIM(USU.ID_USUARIO)
                    INNER JOIN BITACORA.T_BITM_PERSONAL PER ON TRIM(PER.ID_PERSONAL) = TRIM(USU.ID_PERSONA)
                    LEFT JOIN BITACORA.T_BITJ_PERSONAL_OFICINA PO ON TRIM(PO.ID_PERSONAL) = TRIM(PER.ID_PERSONAL)
                    LEFT JOIN BITACORA.T_BITL_OFICINA OFI ON OFI.ID_OFICINA = PO.ID_OFICINA
   
                     INNER JOIN SEGURIDAD.T_SEGD_SISTEMAS_PERFIL SP ON SP.ID_SISTEMA_PERFIL = SPU.ID_PERFIL
                                                                  AND SP.ID_SISTEMA = PI_ID_SISTEMA
                     
                WHERE
                    UPPER(TRIM(USU.COD_USUARIO)) = UPPER(TRIM(PI_USUARIO))
                    AND SPU.ID_SISTEMA = PI_ID_SISTEMA
                    AND USU.FLG_ESTADO='1';
END;



PROCEDURE USP_MENU_USUARIO_PERFIL_LISTAR
(

  PI_ID_USUARIO NUMBER,
  PI_ID_SISTEMA NUMBER,
  PI_ID_PERFIL NUMBER, 
  PI_Results  OUT  sys_refcursor
)
AS
BEGIN
  OPEN PI_Results FOR
    SELECT
        SM.ID_SISTEMA_MODULO,
        SM.ID_SISTEMA_MODULO_PADRE,
        SM.ID_TIPO_MODULO,
        SM.ID_A,
        SM.ID_LI,
        SM.DESC_MODULO,
        SM.IMAGEN,
        SM.ORDEN,
        SM.URL_MODULO
    FROM
        SEGURIDAD.T_SEGJ_SISTEMAS_PERFIL_USUARIO SPU
        INNER JOIN SEGURIDAD.T_SEGJ_SISTEMAS_PERFIL_MODULO SPM ON SPM.ID_PERFIL = SPU.ID_PERFIL
                                                                  AND SPM.ID_SISTEMA = SPU.ID_SISTEMA
                                                                  AND SPM.FLG_ESTADO = 1
        INNER JOIN SEGURIDAD.T_SEGD_SISTEMAS_MODULO SM ON SM.ID_SISTEMA_MODULO = SPM.ID_MODULO
                                                          AND SM.FLG_ESTADO = 1
   WHERE
        SPU.ID_SISTEMA = PI_ID_SISTEMA
        AND SPU.ID_USUARIO = PI_ID_USUARIO
        AND SPU.ID_PERFIL = PI_ID_PERFIL
        AND SPU.Flg_Estado = '1'
   ORDER BY
        SM.ORDEN ASC;

END;


END ;
/



