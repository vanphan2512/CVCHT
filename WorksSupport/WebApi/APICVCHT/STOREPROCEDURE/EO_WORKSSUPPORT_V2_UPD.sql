create or replace PROCEDURE EO_WORKSSUPPORT_V2_UPD
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	08/06/2017
	DESCRIPTION	:	C?P NH?T TH�NG TIN NH�M C�NG VI?C C?N H? TR? 
*/ (
    V_WORKSSUPPORTID            IN EO_WORKSSUPPORT.WORKSSUPPORTID%TYPE,
    V_EXPECTEDCOMPLETEDDATE     IN EO_WORKSSUPPORT.EXPECTEDCOMPLETEDDATE%TYPE,
    V_CONTENT               IN EO_WORKSSUPPORT.CONTENT%TYPE,
    V_UPDATEDUSER               IN EO_WORKSSUPPORT.UPDATEDUSER%TYPE
)
    AS
BEGIN
    UPDATE EO_WORKSSUPPORT
        SET
            EXPECTEDCOMPLETEDDATE = V_EXPECTEDCOMPLETEDDATE,
            CONTENT = V_CONTENT,
            UPDATEDUSER = V_UPDATEDUSER,
            UPDATEDDATE = SYSDATE
    WHERE
        WORKSSUPPORTID = V_WORKSSUPPORTID;
END;