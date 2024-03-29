USE [FlightScheduleManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetInstructorTypeList]    Script Date: 19-10-2021 11:58:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetInstructorTypeList]  
(       
    @SearchValue NVARCHAR(50) = NULL,  
    @PageNo INT = 1,  
    @PageSize INT = 10,  
    @SortColumn NVARCHAR(20) = 'Name',  
    @SortOrder NVARCHAR(20) = 'ASC'  
)  
AS BEGIN  
    SET NOCOUNT ON;  
  
    SET @SearchValue = LTRIM(RTRIM(@SearchValue))  
  
    ; WITH CTE_Results AS   
    (  
        SELECT * from InstructorTypes 
  
        WHERE  @SearchValue= '' OR  (   
              Name LIKE '%' + @SearchValue + '%' 
            )  
  
            ORDER BY  
            CASE WHEN (@SortColumn = 'Name' AND @SortOrder='ASC')  
                        THEN [Name]  
            END ASC,
			CASE WHEN (@SortColumn = 'Name' AND @SortOrder='DESC')  
                        THEN [Name]  
            END DESC

            OFFSET @PageSize * (@PageNo - 1) ROWS  
            FETCH NEXT @PageSize ROWS ONLY  
    ),  
    CTE_TotalRows AS   
    (  
        select count(ID) as TotalRecords from InstructorTypes 
		
        WHERE  (@SearchValue= '' OR  (   
              Name LIKE '%' + @SearchValue + '%' 
            ))   
    )  
    Select TotalRecords, Id, Name from CTE_Results 
	, CTE_TotalRows   
    WHERE EXISTS (SELECT 1 FROM CTE_Results WHERE CTE_Results.ID = ID)  
   
END