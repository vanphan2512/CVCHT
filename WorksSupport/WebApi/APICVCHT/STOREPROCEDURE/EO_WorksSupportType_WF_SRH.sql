CREATE OR REPLACE PROCEDURE ERP.EO_WorksSupportType_WF_SRH
/*
	CREATED BY	:	NGUYEN THI KIM NGAN
	DATE		:	02.06.2017
	DESCRIPTION	:	N?P DANH S�CH C�C THU?C T�NH BU?C X? L� C�NG VI?C C?N H? TR? 
*/
(
    V_OUT OUT SYS_REFCURSOR,
    V_KEYWORDS NVARCHAR2 DEFAULT NULL,
    V_ISDELETED EO_WORKSSUPPORTTYPE_WORKFLOW.ISDELETED%TYPE DEFAULT 0
) 
AS
BEGIN
    OPEN V_OUT FOR
	SELECT *
	FROM 
    EO_WORKSSUPPORTTYPE_WORKFLOW
	WHERE 
    eo_workssupporttype_workflow.isdeleted = V_ISDELETED
    AND (V_KEYWORDS IS NULL 
         OR eo_workssupporttype_workflow.workssupportstepid LIKE '%' || V_KEYWORDS || '%'
         OR UPPER ( eo_workssupporttype_workflow.WORKSSUPPORTSTEPNAME ) LIKE '%' ||UPPER ( V_KEYWORDS )|| '%')
  ORDER BY 
    EO_WORKSSUPPORTTYPE_WORKFLOW.ORDERINDEX
;
END;
/