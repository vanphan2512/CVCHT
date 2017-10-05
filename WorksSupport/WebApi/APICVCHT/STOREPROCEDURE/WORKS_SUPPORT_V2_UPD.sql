CREATE OR REPLACE PROCEDURE ERP.WORKS_SUPPORT_V2_UPD
/*
	Created by	:	Nguyen thi kim ngan
	Date		:	27.07.2017
	Description	:	C?p nh?t th�ng tin C�ng vi?c c?n h? tr? 
*/
(
	v_WORKSSUPPORTID IN EO_WORKSSUPPORT.WORKSSUPPORTID%TYPE,
	V_WORKSSUPPORTNAME IN EO_WORKSSUPPORT.WORKSSUPPORTNAME%TYPE,
	V_CONTENT IN NVARCHAR2 DEFAULT Null,
  V_WORKSSUPPORTGROUPID IN EO_WORKSSUPPORT.WORKSSUPPORTGROUPID%TYPE,
	V_WORKSSUPPORTPRIORITYID IN EO_WORKSSUPPORT.WORKSSUPPORTPRIORITYID%TYPE,
	V_EXPECTEDCOMPLETEDDATE IN EO_WORKSSUPPORT.EXPECTEDCOMPLETEDDATE%TYPE,
	V_UPDATEDUSER IN EO_WORKSSUPPORT.UPDATEDUSER%TYPE,
	V_CERTIFICATESTRING IN EO_WORKSSUPPORT_LOG.CERTIFICATESTRING%TYPE,
	V_USERHOSTADDRESS IN EO_WORKSSUPPORT_LOG.USERHOSTADDRESS%TYPE,
	V_LOGINLOGID IN EO_WORKSSUPPORT_LOG.LOGINLOGID%TYPE

) 
AS
BEGIN
  UPDATE EO_WORKSSUPPORT
    SET 
      WORKSSUPPORTNAME = V_WORKSSUPPORTNAME,
      CONTENT = V_CONTENT,
      WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID,
      WORKSSUPPORTPRIORITYID = V_WORKSSUPPORTPRIORITYID,
      EXPECTEDCOMPLETEDDATE = V_EXPECTEDCOMPLETEDDATE,
      UPDATEDUSER = V_UPDATEDUSER
    WHERE WORKSSUPPORTID = V_WORKSSUPPORTID ;
  
	INSERT
	INTO EO_WORKSSUPPORT_LOG
	(
		WORKSSUPPORTID,
		WORKSSUPPORTTYPEID,
		WORKSSUPPORTNAME,
		CONTENT,
    WORKSSUPPORTSTATUSID,
		EXPECTEDCOMPLETEDDATE,
		UPDATEDUSER,
		UPDATEDDATE,
		LOGTYPE,
		USERHOSTADDRESS,
		CERTIFICATESTRING,
		LOGINLOGID

	)
	VALUES
	(
		v_WORKSSUPPORTID,
		0,
		v_WORKSSUPPORTNAME,
		v_CONTENT,
    1,
		v_EXPECTEDCOMPLETEDDATE,
		V_UPDATEDUSER,
		SYSDATE,
		2,
		v_USERHOSTADDRESS,
		v_CERTIFICATESTRING,
		v_LOGINLOGID

	);
	
END;
/