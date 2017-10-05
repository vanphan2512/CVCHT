create or replace PROCEDURE EO_WORKSSUPPORT_V2_SOLUTION
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	08/06/2017
	DESCRIPTION	:	C?P NH?T TH�NG TIN NH�M C�NG VI?C C?N H? TR? 
*/ (
    V_WORKSSUPPORTID            IN EO_WORKSSUPPORT.WORKSSUPPORTID%TYPE,
    V_SOLUTIONCONTENT           IN EO_WORKSSUPPORT.SOLUTIONCONTENT%TYPE,
    V_UPDATESOLUTIONUSER        IN EO_WORKSSUPPORT.UPDATESOLUTIONUSER%TYPE
)
    AS
BEGIN
    UPDATE EO_WORKSSUPPORT
        SET
            SOLUTIONCONTENT = V_SOLUTIONCONTENT,
            UPDATESOLUTIONUSER = V_UPDATESOLUTIONUSER,
            UPDATESOLUTIONDATE = SYSDATE,
            UPDATEDUSER = V_UPDATESOLUTIONUSER,
            UPDATEDDATE = SYSDATE
    WHERE
        WORKSSUPPORTID = V_WORKSSUPPORTID;
END;