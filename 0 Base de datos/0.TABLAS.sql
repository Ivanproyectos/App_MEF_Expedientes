----------------------------------------------
-- Export file for user EXPEDIENTES         --
-- Created by ivans on 11/11/2021, 6:12:14  --
----------------------------------------------

--prompt 
--prompt  Creating table T_EXPM_DOMINIO
--prompt  =============================
--prompt 
create table EXPEDIENTES.T_EXPM_DOMINIO
(
  ID_DOMINIO         NUMBER not null,
  ID_DOMINIO_PADRE   NUMBER,
  COD_DOMINIO        VARCHAR2(100),
  NOM_DOMINIO        VARCHAR2(100),
  DESC_CORTA_DOMINIO VARCHAR2(200),
  DESC_LARGA_DOMINIO VARCHAR2(1000),
  FLG_ESTADO         INTEGER default 1,
  USU_CREACION       VARCHAR2(50),
  FEC_CREACION       DATE default SYSDATE,
  IP_CREACION        VARCHAR2(50),
  USU_MODIFICACION   VARCHAR2(50),
  FEC_MODIFICACION   DATE,
  IP_MODIFICACION    VARCHAR2(50)
)

alter table EXPEDIENTES.T_EXPM_DOMINIO
  add constraint KEY_DOMINIO primary key (ID_DOMINIO)


--prompt 
--prompt  Creating table T_EXPM_EXPEDIENTES
--prompt  =================================
--prompt 
create table EXPEDIENTES.T_EXPM_EXPEDIENTES
(
  ID_EXPEDIENTE             NUMBER not null,
  ID_PERSONAL               VARCHAR2(6),
  COD_EXPEDIENTE            VARCHAR2(9),
  FECHA_RECEPCION           DATE,
  FECHA_PRESCRIPCION        DATE,
  FECHA_HECHO               DATE,
  HOJA_RUTA                 VARCHAR2(200),
  ID_REMITENTE              NUMBER,
  ID_ACTO                   NUMBER,
  OBSERVACION_INVESTIGADORA VARCHAR2(1000),
  ID_FALTA                  NUMBER,
  ARTICULO                  VARCHAR2(200),
  INC                       VARCHAR2(200),
  OBSERVACION_INSTRUCTORA   VARCHAR2(1000),
  ID_PRECALIFICACION        NUMBER,
  TIPO_SANCION_RECOMENDADA  VARCHAR2(200),
  ACTO_INICIO               VARCHAR2(100),
  FECHA_NOTIFICACION        DATE,
  RECOMENDACION_PREINFORME  VARCHAR2(200),
  ID_SANCION_RECOMENDADA    NUMBER,
  ID_ORGANO_INSTRUCTOR      NUMBER,
  FECHA_NOTIFICACION_INICIO DATE,
  DOCUMENTO_FINALIZACION    VARCHAR2(200),
  RECOMENDACION_INSTRUCTOR  VARCHAR2(200),
  ID_ORGANO_SANCIONADOR     NUMBER,
  SANCION                   VARCHAR2(200),
  OBSERVACION_SANCIONADORA  VARCHAR2(1000),
  ID_SITUACION              NUMBER,
  ID_ESTADO                 NUMBER,
  FLG_ESTADO                INTEGER default 1,
  USU_CREACION              VARCHAR2(50),
  FEC_CREACION              DATE default SYSDATE,
  IP_CREACION               VARCHAR2(50),
  USU_MODIFICACION          VARCHAR2(50),
  FEC_MODIFICACION          DATE,
  IP_MODIFICACION           VARCHAR2(50)
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint KEY primary key (ID_EXPEDIENTE)

alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint FK_ID_ACTO_DOMINIO foreign key (ID_ACTO)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);
alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint FK_ID_ESTADO_DOMINIO foreign key (ID_ESTADO)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);
alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint FK_ID_FALTA_DOMINIO foreign key (ID_FALTA)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);
alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint FK_ID_PRECALIFCACION_DOMINO foreign key (ID_PRECALIFICACION)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);
alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint FK_ID_SANCION_DOMINIO foreign key (ID_SANCION_RECOMENDADA)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);
alter table EXPEDIENTES.T_EXPM_EXPEDIENTES
  add constraint FK_ID_SITUACION_DOMINIO foreign key (ID_SITUACION)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);

--prompt 
--prompt  Creating table T_EXPD_ADJUNTOS
--prompt  ==============================
--prompt 
create table EXPEDIENTES.T_EXPD_ADJUNTOS
(
  ID_ADJUNTO        NUMBER not null,
  ID_MAESTRA        NUMBER,
  ID_TIPO_ARCHIVO   NUMBER,
  NUMERO            INTEGER,
  FECHA             DATE,
  OBSERVACION       VARCHAR2(1000),
  ARCHIVO_BLOB      BLOB,
  ID_SISTEMA        INTEGER,
  NOMBRE_ARCHIVO    VARCHAR2(100),
  EXTENSION_ARCHIVO VARCHAR2(100),
  FLG_ESTADO        INTEGER default 1,
  USU_CREACION      VARCHAR2(50),
  FEC_CREACION      DATE default SYSDATE,
  IP_CREACION       VARCHAR2(50),
  USU_MODIFICACION  VARCHAR2(50),
  FEC_MODIFICACION  DATE,
  IP_MODIFICACION   VARCHAR2(50)
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table EXPEDIENTES.T_EXPD_ADJUNTOS
  add constraint KEY_ADJUNTOS primary key (ID_ADJUNTO)

alter table EXPEDIENTES.T_EXPD_ADJUNTOS
  add constraint FK_ID_MESTRA_EXPEDIENTES foreign key (ID_MAESTRA)
  references EXPEDIENTES.T_EXPM_EXPEDIENTES (ID_EXPEDIENTE);
alter table EXPEDIENTES.T_EXPD_ADJUNTOS
  add constraint FK_ID_TIPO_ARCHIVO_DOMINIO foreign key (ID_TIPO_ARCHIVO)
  references EXPEDIENTES.T_EXPM_DOMINIO (ID_DOMINIO);

--prompt 
--prompt  Creating table T_EXPM_FORMATO_CORREO
--prompt  ====================================
--prompt 
create table EXPEDIENTES.T_EXPM_FORMATO_CORREO
(
  ID_FORMATO       NUMBER not null,
  ASUNTO           VARCHAR2(500) not null,
  BODY             VARCHAR2(4000),
  FLG_ESTADO       INTEGER default 1,
  USU_CREACION     VARCHAR2(50),
  FEC_CREACION     DATE default SYSDATE,
  IP_CREACION      VARCHAR2(50),
  USU_MODIFICACION VARCHAR2(50),
  FEC_MODIFICACION DATE,
  IP_MODIFICACION  VARCHAR2(50)
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table EXPEDIENTES.T_EXPM_FORMATO_CORREO
  add primary key (ID_FORMATO)



