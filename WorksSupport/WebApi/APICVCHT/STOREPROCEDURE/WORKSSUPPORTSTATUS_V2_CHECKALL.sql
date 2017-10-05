create or replace PROCEDURE     WORKSSUPPORTSTATUS_V2_CHECKALL
/*
	CREATED BY	:	TRUONG HOANG NHI
	DATE		:	08/08/2017
	DESCRIPTION	:	KIEM TRA DATA CO CAC TRANG THAI (ISINITSTATUS, ISCOMPLETESTATUS, ISCLOSESTATUS)  
*/
(
    V_OUT OUT SYS_REFCURSOR
) 
AS
BEGIN
   OPEN V_OUT FOR
   SELECT
    DISTINCT
        (
            SELECT
                DISTINCT ISINITSTATUS
            FROM 
                EO_WORKSSUPPORTSTATUS
            WHERE
                ISINITSTATUS = 1
            AND
                ISDELETED = 0
            AND
                ISACTIVE = 1
        ) AS ISINITSTATUS,
        
        (
            SELECT
                DISTINCT ISCOMPLETESTATUS
            FROM 
                EO_WORKSSUPPORTSTATUS
            WHERE
                ISCOMPLETESTATUS = 1
            AND
                ISDELETED = 0
            AND
                ISACTIVE = 1
        ) AS ISCOMPLETESTATUS,
        
        (
            SELECT
                DISTINCT ISCLOSESTATUS
            FROM 
                EO_WORKSSUPPORTSTATUS
            WHERE
                ISCLOSESTATUS = 1
            AND
                ISDELETED = 0
            AND
                ISACTIVE = 1
        ) AS ISCLOSESTATUS

FROM 
    EO_WORKSSUPPORTSTATUS;
END;
