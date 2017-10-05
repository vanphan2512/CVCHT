CREATE OR REPLACE PROCEDURE ERP.EO_WORKSSUPPORT_CMT_ATMENT_DEL
/*
	CREATED BY	:	LUONG TRUNG NH�N 
	DATE		:	21/06/2017
	DESCRIPTION	:	X�A TH�NG TIN DANH S�CH FILE D�NH K�M C?A C�NG VI?C C?N H? TR? 
*/
(
	V_ATTACHMENTID IN EO_WORKSSUPPORT_CMT_ATTACHMENT.ATTACHMENTID%TYPE,
	V_DELETEDUSER IN EO_WORKSSUPPORT_CMT_ATTACHMENT.DELETEDUSER%TYPE
)
AS
BEGIN
	UPDATE EO_WORKSSUPPORT_CMT_ATTACHMENT
	SET
		ISDELETED = 1,
		DELETEDDATE = SYSDATE,
		DELETEDUSER = V_DELETEDUSER
	WHERE
		ATTACHMENTID = V_ATTACHMENTID;
END;
/