----------------------------------------------
-- Export file for user EXPEDIENTES         --
-- Created by ivans on 08/11/2021, 1:20:49  --
----------------------------------------------



--prompt Creating sequence SEQ_ID_ADJUNTO
--prompt ================================

create sequence EXPEDIENTES.SEQ_ID_ADJUNTO
minvalue 1
maxvalue 9999999999999999999999999
start with 9
increment by 1
nocache;


--prompt Creating sequence SEQ_ID_DOMINIO
--prompt ================================

create sequence EXPEDIENTES.SEQ_ID_DOMINIO
minvalue 1
maxvalue 999999999999999999999999
start with 43
increment by 1
nocache;


--prompt Creating sequence SEQ_ID_EXPEDIENTES
--prompt ====================================

create sequence EXPEDIENTES.SEQ_ID_EXPEDIENTES
minvalue 1
maxvalue 999999999999999999999999
start with 11
increment by 1
nocache;


--prompt Creating sequence SEQ_ID_FORMATOCORREO
--prompt ======================================

create sequence EXPEDIENTES.SEQ_ID_FORMATOCORREO
minvalue 1
maxvalue 9999999999999999999999999
start with 4
increment by 1
nocache;


