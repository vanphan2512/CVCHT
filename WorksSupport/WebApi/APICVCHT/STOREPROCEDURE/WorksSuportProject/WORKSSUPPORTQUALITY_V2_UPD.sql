CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTQUALITY_V2_UPD
/*
	CREATED BY	:	LƯƠNG TRUNG NHÂN
	DATE		:	30/05/2017
	DESCRIPTION	:	CẬP NHẬT THÔNG TIN TRẠNG THÁI CÔNG VIỆC CẦN HỖ TRỢ 
*/
(
  V_OUT OUT SYS_REFCURSOR,
	V_WORKSSUPPORTQUALITYID IN EO_WORKSSUPPORTQUALITY.WORKSSUPPORTQUALITYID%TYPE,
	V_WORKSSUPPORTQUALITYNAME IN EO_WORKSSUPPORTQUALITY.WORKSSUPPORTQUALITYNAME%TYPE,
	V_DESCRIPTION IN EO_WORKSSUPPORTQUALITY.DESCRIPTION%TYPE,
  V_ICONURL IN EO_WORKSSUPPORTQUALITY.ICONURL%TYPE,
	V_ORDERINDEX IN EO_WORKSSUPPORTQUALITY.ORDERINDEX%TYPE,
	V_ISACTIVE IN EO_WORKSSUPPORTQUALITY.ISACTIVE%TYPE,
	V_ISSYSTEM IN EO_WORKSSUPPORTQUALITY.ISSYSTEM%TYPE,
	V_UPDATEDUSER IN EO_WORKSSUPPORTQUALITY.UPDATEDUSER%TYPE,
	V_CERTIFICATESTRING IN EO_WORKSSUPPORTQUALITY_LOG.CERTIFICATESTRING%TYPE,
	V_USERHOSTADDRESS IN EO_WORKSSUPPORTQUALITY_LOG.USERHOSTADDRESS%TYPE,
	V_LOGINLOGID IN EO_WORKSSUPPORTQUALITY_LOG.LOGINLOGID%TYPE

) 
AS
BEGIN
  
    UPDATE EO_WORKSSUPPORTQUALITY
  	SET
  		WORKSSUPPORTQUALITYNAME	= V_WORKSSUPPORTQUALITYNAME,
      orderindex = v_orderindex,
  		DESCRIPTION	= V_DESCRIPTION,
      ICONURL = V_ICONURL,
  		ISACTIVE	= V_ISACTIVE,
  		ISSYSTEM	= V_ISSYSTEM,
  		UPDATEDUSER	= V_UPDATEDUSER,
  		UPDATEDDATE	= SYSDATE
  	WHERE
  		WORKSSUPPORTQUALITYID = V_WORKSSUPPORTQUALITYID;
   
	INSERT
	INTO EO_WORKSSUPPORTQUALITY_LOG
	(
		WORKSSUPPORTQUALITYID,
		WORKSSUPPORTQUALITYNAME,
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
		V_WORKSSUPPORTQUALITYID,
		V_WORKSSUPPORTQUALITYNAME,
    V_DESCRIPTION,
		V_ORDERINDEX,
		V_ISACTIVE,
		V_ISSYSTEM,
		V_UPDATEDUSER,
		SYSDATE,
		2,
		V_USERHOSTADDRESS,
		V_CERTIFICATESTRING,
		V_LOGINLOGID
	);
	 OPEN V_OUT FOR 
    SELECT * 
      FROM EO_WORKSSUPPORTQUALITY WHERE  WORKSSUPPORTQUALITYID = V_WORKSSUPPORTQUALITYID;
    RETURN;
END;
/