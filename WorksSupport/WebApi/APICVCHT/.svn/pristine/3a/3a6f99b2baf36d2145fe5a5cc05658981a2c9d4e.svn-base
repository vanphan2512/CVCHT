create or replace PROCEDURE  EO_WORKSSUPPORT_SUPPORTID_DEL
/*
	Created by	:	Lýõng Trung Nhân
	Date		:	12/06/2017
	Description	:	Xóa thông tin quy?n trên d? án công vi?c c?n h? tr?
*/
(
	V_WORKSSUPPORTID IN EO_WORKSSUPPORT_WORKS.WORKSSUPPORTID%TYPE
)
AS
BEGIN
	DELETE 
        EO_WORKSSUPPORT_WORKS
	WHERE
		WORKSSUPPORTID = V_WORKSSUPPORTID;
END;