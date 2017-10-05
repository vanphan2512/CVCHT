create or replace PROCEDURE EO_WORKSSUPPORTTYPE_WFNX_SEL
/*
	CREATED BY	:	L��NG TRUNG NH�N 
	DATE		:	02/06/2017
	DESCRIPTION	:	N?P TH�NG TIN B?NG CH?A C�C B�?C X? L? K? TI?P
*/ (
    V_WORKSSUPPORTTYPEID   IN EO_WORKSSUPPORTTYPE_WF_NX.WORKSSUPPORTTYPEID%TYPE,
    V_WORKSSUPPORTSTEPID   IN EO_WORKSSUPPORTTYPE_WF_NX.WORKSSUPPORTSTEPID%TYPE,
    V_OUT                  OUT SYS_REFCURSOR
)
    AS
BEGIN
    OPEN V_OUT FOR
        SELECT
            WORKSSUPPORTTYPEID,
            NEXTWORKSSUPPORTSTEPID,            
            WORKSSUPPORTSTEPID,            
            CHOOSEFUNCTIONID
        FROM
            EO_WORKSSUPPORTTYPE_WF_NX
        WHERE
            WORKSSUPPORTTYPEID = V_WORKSSUPPORTTYPEID
        AND
            WORKSSUPPORTSTEPID = V_WORKSSUPPORTSTEPID;

END;