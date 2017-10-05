CREATE OR REPLACE PROCEDURE ERP.EO_WORKS_GET_WORKSSUPPORTID
/*
	CREATED BY	:	Nguyen Van Huan
	DATE		:	29.06.2017
	DESCRIPTION	:	lay tat ca thanh vien trong nhom cong viec
*/
(	 
  V_WORKSSUPPORTID IN EO_WORKSSUPPORT_WORKS.WORKSSUPPORTID%TYPE,
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
 OPEN V_OUT FOR
    SELECT
        EO_WORKS.WORKSID,
        EO_WORKS.WORKSNAME,
        EO_WORKS.CURRENTPROGRESS,
        EO_WORKS.STARTDATE,
        EO_WORKS.ENDDATE
    FROM
            EO_WORKS EO_WORKS
        INNER JOIN 
            EO_WORKSSUPPORT_WORKS WORKS 
        ON
            EO_WORKS.WORKSID = WORKS.WORKSID
        AND
            EO_WORKS.ISDELETED = 0
        AND
            WORKS.WORKSSUPPORTID = V_WORKSSUPPORTID;                                                                                                                                                                                                                                                                                                                                                                    
END;
/