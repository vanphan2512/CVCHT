create or replace PROCEDURE  SOLUTION_ATTACHMENT_DELID
/*
	CREATED BY	:	L��NG TRUNG NH�N 
	DATE		:	21/06/2017
	DESCRIPTION	:	X�A TH�NG TIN DANH S�CH FILE D�NH K�M C?A C�NG VI?C C?N H? TR? 
*/
(
	V_ATTACHMENTID IN EO_WORKSSUPPORT_SOLUTIONATM.ATTACHMENTID%TYPE,
	V_DELETEDUSER IN EO_WORKSSUPPORT_SOLUTIONATM.DELETEDUSER%TYPE
)
AS
BEGIN
	UPDATE EO_WORKSSUPPORT_SOLUTIONATM
	SET
		ISDELETED = 1,
		DELETEDDATE = SYSDATE,
		DELETEDUSER = V_DELETEDUSER
	WHERE
		ATTACHMENTID = V_ATTACHMENTID;
END;