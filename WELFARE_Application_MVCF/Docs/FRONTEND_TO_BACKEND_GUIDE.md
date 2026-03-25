# ✅ Frontend to Backend Data Flow - Complete Guide

## 🎯 Your Setup is Now Ready!

Sample data seeding has been **REMOVED**. Your application now starts with an **empty database** and you can add real data through the frontend forms.

---

## 🔄 How Data Flows: Frontend → Backend → SQL Server

### Complete Data Flow:

```
USER FILLS FORM (Frontend)
        ↓
View (Create.cshtml / Manage.cshtml)
        ↓
POST Request with Form Data
        ↓
Controller (ProgramController.Create)
        ↓
Service Layer (ProgramService.AddProgramAsync)
    → Validates Business Rules
    → Checks dates, budget, duplicates
        ↓
Repository (ProgramRepository.AddProgramAsync)
        ↓
DbContext (_context.Programs.AddAsync)
        ↓
Entity Framework Core
        ↓
SQL INSERT Statement
        ↓
SQL SERVER DATABASE (WelfareLinkDBVCF)
    → Programs Table Updated
        ↓
Success! Data Saved ✅
        ↓
Redirect to Index with Success Message
```

---

## 📝 Step-by-Step: Add Programme Through Frontend

### 1. **Run Your Application**
```powershell
# Press F5 in Visual Studio
# Or in terminal:
dotnet run
```

### 2. **Navigate to Create Programme**
Choose **ONE** of these URLs:

**Option A: PM Dashboard Design**
```
https://localhost:{port}/Program/Manage
```
- Clean form with Add/Update toggle
- Select "Add New Programme" radio button

**Option B: Bootstrap Design**
```
https://localhost:{port}/Program/Create
```
- Modern card-based form

### 3. **Fill in the Form**
Example data:
```
Title: Winter Fuel Subsidy
Description: Provides heating assistance to low-income families during winter
Start Date: 2026-04-01
End Date: 2027-03-31
Budget: 500000
Status: Active (auto-set)
```

### 4. **Click Submit**
- Form validates on client-side
- Sends POST request to controller
- Server validates business rules
- Saves to database
- Redirects with success message

### 5. **Verify Data Saved**

**Option A: Check in UI**
```
Navigate to: /Program/Index
You'll see your newly created programme in the table
```

**Option B: Check in SQL Server**
```sql
-- Open SQL Server Management Studio
-- Connect to: .\SQLEXPRESS
-- Database: WelfareLinkDBVCF

SELECT * FROM Programs;
-- You'll see your programme with ID, Title, Budget, etc.
```

---

## 🧪 Test the Complete Flow

### Test 1: Create Programme (Success)
1. Go to `/Program/Manage` or `/Program/Create`
2. Fill form with valid data
3. Click Submit
4. ✅ Success message appears
5. ✅ Redirected to Index
6. ✅ Programme appears in list
7. ✅ Check SQL Server - data is there!

### Test 2: Business Rule Validation (Fail)
1. Go to `/Program/Manage`
2. Fill form with:
   - Start Date: 2026-12-31
   - End Date: 2026-01-01 (BEFORE start date)
3. Click Submit
4. ❌ Error message: "Programme end date must be after the start date."
5. ❌ Form stays on page
6. ❌ Database NOT updated (correct behavior)

### Test 3: Duplicate Title (Fail)
1. Create programme: "Winter Fuel Subsidy"
2. Try to create another: "Winter Fuel Subsidy"
3. ❌ Error: "A programme with the title 'Winter Fuel Subsidy' already exists."
4. ❌ Database NOT updated

### Test 4: Add Resources
1. Create a programme first
2. Go to `/Resource/AllocateForm`
3. Select the programme
4. Type: Funds
5. Quantity: 200000
6. Click Submit
7. ✅ Resource saved to SQL Server
8. ✅ Linked to programme (ProgramID)

---

## 🔍 Verify Data in SQL Server

### Check Programme Data:
```sql
-- View all programmes
SELECT 
    ProgramID,
    Title,
    StartDate,
    EndDate,
    Budget,
    Status
FROM Programs;
```

### Check Resource Data:
```sql
-- View all resources
SELECT 
    ResourceID,
    ProgramID,
    Type,
    Quantity,
    Status
FROM Resources;
```

### Check Programme with Resources:
```sql
-- View programmes with their resources
SELECT 
    p.ProgramID,
    p.Title AS ProgramTitle,
    p.Budget AS ProgramBudget,
    p.Status AS ProgramStatus,
    r.ResourceID,
    r.Type AS ResourceType,
    r.Quantity,
    r.Status AS ResourceStatus
FROM Programs p
LEFT JOIN Resources r ON p.ProgramID = r.ProgramID
ORDER BY p.ProgramID;
```

---

## 📊 Data Flow Explained

### When You Submit a Form:

#### 1. **View → Controller**
```csharp
// Form POST from Create.cshtml
<form asp-action="Create" method="post">
    <input asp-for="Title" />
    <input asp-for="Budget" />
    <button type="submit">Create</button>
</form>

// Controller receives data
[HttpPost]
public async Task<IActionResult> Create(WelfareProgram program)
{
    // program object contains form data
    if (ModelState.IsValid) // Validates [Required], [StringLength], etc.
    {
        await _programService.AddProgramAsync(program);
        // Data sent to service layer
    }
}
```

#### 2. **Service → Repository**
```csharp
// ProgramService.AddProgramAsync
public async Task AddProgramAsync(WelfareProgram program)
{
    // 1. Validate business rules
    ValidateProgramDates(program);
    ValidateProgramBudget(program);
    await ValidateDuplicateTitle(program.Title, program.ProgramID);
    
    // 2. Set defaults
    program.Status = "Active";
    
    // 3. Send to repository
    await _programRepository.AddProgramAsync(program);
}
```

#### 3. **Repository → Database**
```csharp
// ProgramRepository.AddProgramAsync
public async Task AddProgramAsync(WelfareProgram program)
{
    // 1. Add to DbContext (in-memory)
    await _context.Programs.AddAsync(program);
    
    // 2. Save changes to SQL Server
    await _context.SaveChangesAsync();
    // ↑ This line executes SQL INSERT statement
}
```

#### 4. **Entity Framework Generates SQL**
```sql
-- EF Core generates and executes:
INSERT INTO Programs (Title, Description, StartDate, EndDate, Budget, Status)
VALUES ('Winter Fuel Subsidy', 'Heating assistance...', '2026-04-01', '2027-03-31', 500000, 'Active');

-- SQL Server saves the data
-- New ProgramID is auto-generated (IDENTITY column)
```

---

## ✅ What Happens When You Edit

### Same Flow for Updates:

```
USER EDITS FORM (Frontend)
        ↓
Edit.cshtml sends POST to Controller
        ↓
ProgramController.Edit(id, program)
        ↓
ProgramService.UpdateProgramAsync(program)
    → Validates changes
        ↓
ProgramRepository.UpdateProgramAsync(program)
        ↓
_context.Programs.Update(program)
        ↓
_context.SaveChangesAsync()
        ↓
SQL UPDATE Statement
        ↓
SQL SERVER - Record Updated ✅
```

### SQL Generated:
```sql
UPDATE Programs 
SET 
    Title = 'Updated Title',
    Budget = 750000,
    Status = 'Suspended'
WHERE ProgramID = 1;
```

---

## 🎯 Key Points

### ✅ Data Persistence:
- All data you enter through frontend forms is **permanently saved** in SQL Server
- No need to edit migration files
- Database changes are made through EF Core

### ✅ Validation Layers:
1. **Client-side:** HTML5 validation (required, date, number)
2. **Model validation:** Data Annotations ([Required], [StringLength])
3. **Business validation:** Service layer (dates, budget, duplicates)
4. **Database constraints:** SQL Server (Primary Key, Foreign Key)

### ✅ Your Current Setup:
- ✅ Migrations applied - Tables created in SQL Server
- ✅ DbInitializer removed - No sample data
- ✅ Controllers connected to Services
- ✅ Services connected to Repositories
- ✅ Repositories connected to DbContext
- ✅ DbContext connected to SQL Server
- ✅ Forms ready to accept input
- ✅ Data will save to database

---

## 🚀 Ready to Go!

### Quick Test:
1. **Run:** Press F5
2. **Navigate:** `/Program/Manage`
3. **Add Programme:** Fill form and submit
4. **Verify UI:** Go to `/Program/Index` - see your programme
5. **Verify DB:** Open SSMS and run `SELECT * FROM Programs`

**Your frontend form inputs will now save directly to SQL Server!** 🎉

---

## 📋 Checklist

- [x] Remove sample data seeding from Program.cs
- [x] Database tables created via migration
- [x] Controllers handle POST requests
- [x] Services validate business rules
- [x] Repositories save to database
- [x] Forms configured with asp-for helpers
- [x] Success/error messages configured
- [x] Ready for real data entry

**Status:** ✅ Application Ready for Production Data Entry

No sample data will be created. All data comes from your frontend forms! 👍
