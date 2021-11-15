----------------------------------------------
-- Export file for user APP_GESEXPDIS         --
-- Created by ivans on 15/11/2021, 2:26:06  --


--prompt
--prompt Creating sequence SEQ_ID_ADJUNTO
--prompt ================================
--prompt
create sequence APP_GESEXPDIS.SEQ_ID_ADJUNTO
minvalue 1
maxvalue 9999999999999999999999999
start with 12
increment by 1
nocache;

--prompt
--prompt Creating sequence SEQ_ID_DOMINIO
--prompt ================================
--prompt
create sequence APP_GESEXPDIS.SEQ_ID_DOMINIO
minvalue 1
maxvalue 999999999999999999999999
start with 51
increment by 1
nocache;

--prompt
--prompt Creating sequence SEQ_ID_EXPEDIENTES
--prompt ====================================
--prompt
create sequence APP_GESEXPDIS.SEQ_ID_EXPEDIENTES
minvalue 1
maxvalue 999999999999999999999999
start with 17
increment by 1
nocache;

--prompt
--prompt Creating sequence SEQ_ID_FORMATOCORREO
--prompt ======================================
--prompt
create sequence APP_GESEXPDIS.SEQ_ID_FORMATOCORREO
minvalue 1
maxvalue 9999999999999999999999999
start with 9
increment by 1
nocache;

