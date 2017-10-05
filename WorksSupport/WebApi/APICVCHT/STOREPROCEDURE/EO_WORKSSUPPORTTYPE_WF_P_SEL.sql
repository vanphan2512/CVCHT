create or replace PROCEDURE     EO_WORKSSUPPORTTYPE_WF_P_SEL
/*
	CREATED BY	:	L��NG TRUNG NH�N 
	DATE		:	02/06/2017
	DESCRIPTION	:	N?P TH�NG TIN B?NG CH?A C�C B�?C X? L? K? TI?P
*/
(	
	V_WORKSSUPPORTSTEPID IN EO_WORKSSUPPORTTYPE_WF_PERMIS.WORKSSUPPORTSTEPID%TYPE,	
--    V_WORKSSUPPORTMEMBERROLEID IN EO_WORKSSUPPORTTYPE_WF_PERMIS.WORKSSUPPORTMEMBERROLEID%TYPE,
	V_OUT OUT SYS_REFCURSOR
) 
AS
BEGIN
    OPEN V_OUT FOR
	SELECT
		WORKSSUPPORTSTEPID,
		WORKSSUPPORTMEMBERROLEID,
        ISCANADDATTACHMENT,
        ISCANCHANGEPROGRESS,
        ISCANCOMMENT,
        ISCANEDITCONTENT,
        ISCANEDITEXPECTEDCOMPLETEDDATE,
        ISCANEDITQUALITY,
        ISCANEDITSOLUTIONCONTENT,
        ISCANPROCESSWORKFLOW,
        ISMUSTCHOOSEPROCESSUSER
	FROM 
    EO_WORKSSUPPORTTYPE_WF_PERMIS
	WHERE
        V_WORKSSUPPORTSTEPID = WORKSSUPPORTSTEPID
--        AND 
--        V_WORKSSUPPORTMEMBERROLEID = WORKSSUPPORTMEMBERROLEID
;
END;