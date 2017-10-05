CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTPRIORITY_V2_ADD
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	16/12/2015 
	DESCRIPTION	:	TH�M TH�NG TIN MUC DO XU LY
*/
(
  V_OUT OUT NUMBER,
	--V_WORKSSUPPORTPRIORITYID IN EO_WORKSSUPPORTPRIORITY.WORKSSUPPORTPRIORITYID%TYPE,
	V_WORKSSUPPORTPRIORITYNAME IN EO_WORKSSUPPORTPRIORITY.WORKSSUPPORTPRIORITYNAME%TYPE,
  V_ICONURL IN EO_WORKSSUPPORTPRIORITY.ICONURL%TYPE,
  V_COLORCODE IN EO_WORKSSUPPORTPRIORITY.COLORCODE%TYPE,
	V_DESCRIPTION IN EO_WORKSSUPPORTPRIORITY.DESCRIPTION%TYPE,
	V_ORDERINDEX IN EO_WORKSSUPPORTPRIORITY.ORDERINDEX%TYPE,
	V_ISACTIVE IN EO_WORKSSUPPORTPRIORITY.ISACTIVE%TYPE,
	V_ISSYSTEM IN EO_WORKSSUPPORTPRIORITY.ISSYSTEM%TYPE,
	V_CREATEDUSER IN EO_WORKSSUPPORTPRIORITY.CREATEDUSER%TYPE,
--	V_CREATEDDATE IN EO_WORKSSUPPORTPRIORITY.CREATEDDATE%TYPE,
 -- V_UPDATEDUSER IN EO_WORKSSUPPORTPRIORITY.UPDATEDUSER%TYPE,
	--V_UPDATEDDATE IN EO_WORKSSUPPORTPRIORITY.UPDATEDDATE%TYPE,
  --V_ISDELETED IN EO_WORKSSUPPORTPRIORITY.ISDELETED%TYPE,
  --V_DELETEDUSER IN EO_WORKSSUPPORTPRIORITY.DELETEDUSER%TYPE,
  --V_DELETEDDATE IN EO_WORKSSUPPORTPRIORITY.DELETEDDATE%TYPE,
  V_CERTIFICATESTRING IN EO_WORKSSUPPORTPRIORITY_LOG.CERTIFICATESTRING%TYPE,
	V_USERHOSTADDRESS IN EO_WORKSSUPPORTPRIORITY_LOG.USERHOSTADDRESS%TYPE,
	V_LOGINLOGID IN EO_WORKSSUPPORTPRIORITY_LOG.LOGINLOGID%TYPE
)
AS
T_ORDERINDEX EO_WORKSSUPPORTPRIORITY.ORDERINDEX%TYPE;
BEGIN
--   IF V_ISOTHER = 1 THEN  
--    UPDATE EO_EDOCUMENTPROCESSRESULT SET ISOTHER = 0 ;
--   END IF;
  SELECT COUNT(*) + 1 INTO T_ORDERINDEX 
  FROM 
    EO_WORKSSUPPORTPRIORITY
  WHERE ISDELETED = 0;

	INSERT
	INTO EO_WORKSSUPPORTPRIORITY
	(
		--WORKSSUPPORTPRIORITYID,
		WORKSSUPPORTPRIORITYNAME,
		ICONURL,
    COLORCODE,
		DESCRIPTION,
		ORDERINDEX,
		ISACTIVE,
		ISSYSTEM,
		CREATEDUSER,
		CREATEDDATE
    --ISDELETED,
    --DELETEDUSER,
    --DELETEDDATE
	)
	VALUES
	(
		--V_WORKSSUPPORTPRIORITYID,
		V_WORKSSUPPORTPRIORITYNAME,
		V_ICONURL,
    V_COLORCODE,
		V_DESCRIPTION,
		T_ORDERINDEX,
		V_ISACTIVE,
		V_ISSYSTEM,
		V_CREATEDUSER,
		SYSDATE
    --V_ISDELETED,
    --V_DELETEDUSER,
    --V_DELETEDDATE
	)RETURNING WORKSSUPPORTPRIORITYID into V_OUT;

	INSERT
	INTO EO_WORKSSUPPORTPRIORITY_LOG
	(
		--WORKSSUPPORTPRIORITYID,
		WORKSSUPPORTPRIORITYNAME,
		ICONURL,
		DESCRIPTION,
		ORDERINDEX,
		ISACTIVE,
		ISSYSTEM,
    UPDATEDUSER,
		UPDATEDDATE,
	  LOGTYPE,
		USERHOSTADDRESS,
		CERTIFICATESTRING,
		LOGINLOGID
	)
	VALUES
	(
		--id,
		V_WORKSSUPPORTPRIORITYNAME,
		V_ICONURL,
		V_DESCRIPTION,
		V_ORDERINDEX,
		V_ISACTIVE,
		V_ISSYSTEM,
   	V_CREATEDUSER,
		SYSDATE,
	  1,
		V_USERHOSTADDRESS,
		V_CERTIFICATESTRING,
		V_LOGINLOGID
	);

END;
/