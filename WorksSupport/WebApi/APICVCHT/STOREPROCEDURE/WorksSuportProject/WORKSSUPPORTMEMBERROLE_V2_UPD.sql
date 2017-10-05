CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTMEMBERROLE_V2_UPD
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	16/12/2015
	DESCRIPTION	:	CAP NHAT TH�NG TIN MUC DO XU LY
*/
(
  V_OUT OUT SYS_REFCURSOR,
	V_WORKSSUPPORTMEMBERROLEID IN EO_WORKSSUPPORTMEMBERROLE.WORKSSUPPORTMEMBERROLEID%TYPE,
	V_WORKSSUPPORTMEMBERROLENAME IN EO_WORKSSUPPORTMEMBERROLE.WORKSSUPPORTMEMBERROLENAME%TYPE,
	V_DESCRIPTION IN EO_WORKSSUPPORTMEMBERROLE.DESCRIPTION%TYPE,
  V_ICONURL IN EO_WORKSSUPPORTMEMBERROLE.ICONURL%TYPE,
	V_ORDERINDEX IN EO_WORKSSUPPORTMEMBERROLE.ORDERINDEX%TYPE,
	V_ISACTIVE IN EO_WORKSSUPPORTMEMBERROLE.ISACTIVE%TYPE,
	V_ISSYSTEM IN EO_WORKSSUPPORTMEMBERROLE.ISSYSTEM%TYPE,
	V_UPDATEDUSER IN EO_WORKSSUPPORTMEMBERROLE.UPDATEDUSER%TYPE,
	--V_UPDATEDDATE IN EO_WORKSSUPPORTPRIORITY.UPDATEDDATE%TYPE,
  V_CERTIFICATESTRING IN EO_WORKSSUPPORTMEMBERROLE_LOG.CERTIFICATESTRING%TYPE,
	V_USERHOSTADDRESS IN EO_WORKSSUPPORTMEMBERROLE_LOG.USERHOSTADDRESS%TYPE,
	V_LOGINLOGID IN EO_WORKSSUPPORTMEMBERROLE_LOG.LOGINLOGID%TYPE
)
AS
   V_ORDERINDEXOLD NUMBER;
BEGIN
  --1. LAY ODERINDEX CU DUOC DOI CAP NHAT THANH MOI
  SELECT ew.orderindex INTO v_orderindexold FROM EO_WORKSSUPPORTMEMBERROLE ew WHERE workssupportmemberroleid = V_WORKSSUPPORTMEMBERROLEID;
   --2. c?p nh?t orderindex cho b?ng du?c ho�n v? tru?ng orderindex.
  UPDATE EO_WORKSSUPPORTMEMBERROLE
  	  SET
  		orderindex = v_orderindexold,
  		UPDATEDDATE	= sysdate
  WHERE orderindex = v_orderindex AND isdeleted = 0;
  --3. c?p nh?t orderindex cho b?ng du?c thay d�i tru?ng orderindex(d�ng d? li?u dang du?c thay d?i)
	UPDATE EO_WORKSSUPPORTMEMBERROLE
	SET
		WORKSSUPPORTMEMBERROLENAME	= V_WORKSSUPPORTMEMBERROLENAME,
  	DESCRIPTION	= V_DESCRIPTION,
    ICONURL = V_ICONURL,
    ORDERINDEX = V_ORDERINDEX,
		ISACTIVE	= V_ISACTIVE,
		ISSYSTEM	= V_ISSYSTEM,
		UPDATEDDATE	= SYSDATE
	WHERE
		WORKSSUPPORTMEMBERROLEID = V_WORKSSUPPORTMEMBERROLEID;

  INSERT
  INTO EO_WORKSSUPPORTMEMBERROLE_LOG
	(

    WORKSSUPPORTMEMBERROLEID,
		WORKSSUPPORTMEMBERROLENAME,
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
  VALUES(
		V_WORKSSUPPORTMEMBERROLEID,
		V_WORKSSUPPORTMEMBERROLENAME,
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
      FROM EO_WORKSSUPPORTMEMBERROLE WHERE  WORKSSUPPORTMEMBERROLEID = V_WORKSSUPPORTMEMBERROLEID;
    RETURN;
END;
/