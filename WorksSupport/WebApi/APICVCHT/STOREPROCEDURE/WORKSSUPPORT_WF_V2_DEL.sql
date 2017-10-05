CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_WF_V2_DEL
/*
	CREATED BY	:	Luong Trung Nh�n
	DATE		:	21/06/2017
	DESCRIPTION	:	X�A TH�NG TIN TI?N D? C�NG VI?C C?N H? TR? 
*/
(
	V_WORKSSUPPORTID IN EO_WORKSSUPPORT_WORKFLOW.WORKSSUPPORTID%TYPE
)
AS
BEGIN
	UPDATE EO_WORKSSUPPORT_WORKFLOW
	SET
		ISDELETED = 1,
		DELETEDDATE = SYSDATE,
		DELETEDUSER = v_DELETEDUSER
	WHERE (v_WORKSSUPPORTID  <=0 OR WORKSSUPPORTID = v_WORKSSUPPORTID)
    ;
END;
/