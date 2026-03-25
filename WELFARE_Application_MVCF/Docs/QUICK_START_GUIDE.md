# 🚀 Quick Start Guide - WelfareLink Application

## Prerequisites
- ✅ Visual Studio 2026
- ✅ .NET 10 SDK
- ✅ SQL Server Express (already configured)

---

## Step 1: Run the Application

### Option A: Visual Studio
1. Open solution in Visual Studio
2. Press **F5** (Debug) or **Ctrl+F5** (Run without debugging)
3. Browser will open automatically

### Option B: Terminal
```powershell
cd C:\Users\2481427\OneDrive - Cognizant\Documents\VS projects\WELFARE_Application_MVCF
dotnet run
```

---

## Step 2: Access URLs

Your application will run on: `https://localhost:{port}` (check terminal for actual port)

### 🎯 Quick Access URLs:

#### **Main Dashboard:**
```
https://localhost:{port}/Program/Dashboard
```
- Shows KPIs (Total Budget, Allocated, Remaining)
- Lists active programmes
- Sidebar navigation

#### **Programme Management:**
```
List: https://localhost:{port}/Program/Index
Add/Update: https://localhost:{port}/Program/Manage
Create: https://localhost:{port}/Program/Create
```

#### **Resource Management:**
```
Allocate: https://localhost:{port}/Resource/AllocateForm
List: https://localhost:{port}/Resource/Index
By Program: https://localhost:{port}/Resource/ManageResources?programId=1
```

#### **Dashboards:**
```
Budget Monitoring: https://localhost:{port}/Program/BudgetMonitoring
Performance: https://localhost:{port}/Program/Performance
Utilisation Report: https://localhost:{port}/Resource/UtilisationReport
```

---

## Step 3: First-Time Setup (Sample Data)

### Automatic Sample Data:
If you added `DbInitializer.cs`, sample data will be created automatically on first run:
- 4 sample programmes
- 4 sample resources

### Manual Data Entry:
1. Go to `/Program/Manage`
2. Select "Add New Programme"
3. Fill in:
   ```
   Title: Winter Fuel Subsidy
   Description: Heating assistance programme
   Start Date: 2026-01-01
   End Date: 2026-12-31
   Budget: 500000
   Status: Active
   ```
4. Click "Create Programme"

---

## Step 4: Test Workflow

### 🎯 Complete Test Flow:

1. **View Dashboard**
   - URL: `/Program/Dashboard`
   - See KPIs and active programmes

2. **Create Programme**
   - URL: `/Program/Manage`
   - Add new programme

3. **View List**
   - URL: `/Program/Index`
   - Search for programme

4. **Allocate Resources**
   - URL: `/Resource/AllocateForm`
   - Select programme
   - Add funds (e.g., 200000)

5. **Check Budget**
   - URL: `/Program/BudgetMonitoring`
   - See budget utilisation

6. **View Details**
   - URL: `/Program/Details/1`
   - See programme with resources

---

## 📊 Sample Test Data

### Programme 1:
```
Title: Winter Fuel Subsidy
Description: Assistance for heating costs
Start Date: 2026-10-01
End Date: 2027-03-31
Budget: ₹500,000
Status: Active
```

### Programme 2:
```
Title: Housing Support
Description: Low-income housing assistance
Start Date: 2026-01-01
End Date: 2026-12-31
Budget: ₹1,200,000
Status: Active
```

### Resources:
```
Programme: Winter Fuel Subsidy
Type: Funds
Quantity: ₹300,000
Status: Available
```

---

## 🎨 Two UI Options

### Option 1: PM Dashboard (Sidebar Navigation)
- Layout: `_LayoutPM`
- Views: Dashboard, Manage, Index (updated), AllocateForm
- Style: Professional sidebar menu

### Option 2: Bootstrap Modern Design
- Layout: `_Layout`
- Views: Create, Edit, Details, Delete, Monitoring, Performance
- Style: Cards, badges, modern UI

Both are available! Views specify which layout to use.

---

## 🗄️ Database Verification

### Check in SQL Server Management Studio:
```sql
-- Connect to: .\SQLEXPRESS
-- Database: WelfareLinkDBVCF

-- View programmes
SELECT * FROM Programs;

-- View resources
SELECT * FROM Resources;

-- View programme with resources
SELECT 
    p.ProgramID, 
    p.Title, 
    p.Budget, 
    p.Status,
    r.Type,
    r.Quantity,
    r.Status AS ResourceStatus
FROM Programs p
LEFT JOIN Resources r ON p.ProgramID = r.ProgramID;
```

---

## 🔧 Common Issues & Solutions

### Issue: Database not created
**Solution:**
```powershell
Update-Database
# Or
dotnet ef database update
```

### Issue: Port already in use
**Solution:**
- Check `Properties/launchSettings.json`
- Or kill process on port:
```powershell
netstat -ano | findstr :{port}
taskkill /PID {process_id} /F
```

### Issue: CSS not loading
**Solution:**
1. Clear browser cache (Ctrl+Shift+Delete)
2. Hard refresh (Ctrl+F5)
3. Check `wwwroot/css/site.css` exists

### Issue: No programmes displayed
**Solution:**
- Sample data seeded automatically
- Or manually create via `/Program/Create` or `/Program/Manage`

---

## 📱 Quick Navigation (Once Running)

### Sidebar Menu:
```
Main Dashboard        → Overview
View All Programs     → Search & List
Add / Update Program  → Create/Update
Resource Allocation   → Allocate Resources
Utilization Overview  → Reports
Budget Monitoring     → Budget Dashboard
Performance Dashboard → KPIs
```

---

## ✅ Success Indicators

You'll know it's working when you see:

1. ✅ Dashboard loads with KPI cards
2. ✅ Programme list shows sample data
3. ✅ Sidebar navigation works
4. ✅ Create/Update programme succeeds
5. ✅ Resource allocation validates against budget
6. ✅ Budget monitoring shows utilisation %
7. ✅ Search filters programmes

---

## 🎯 Key Features to Test

### Business Logic Validation:
- ✅ End date must be after start date
- ✅ Start date cannot be in past
- ✅ Budget must be > 0
- ✅ No duplicate programme titles
- ✅ Cannot delete active programmes
- ✅ Funds allocation cannot exceed budget
- ✅ Resources only to active programmes

### UI Features:
- ✅ Sidebar navigation with active highlighting
- ✅ Search functionality on Index page
- ✅ Add/Update mode toggle on Manage page
- ✅ Status badges (Active, Completed, Suspended)
- ✅ Budget utilisation progress bars
- ✅ Critical budget alerts (≥80%)
- ✅ Success/Error messages via TempData

---

## 🚀 Next Steps

After successful run:
1. Test all CRUD operations
2. Verify business validations
3. Check budget calculations
4. Test resource allocation
5. Review dashboards and reports
6. Customize as needed

---

**Ready to run!** Press F5 in Visual Studio and navigate to `/Program/Dashboard` to get started! 🎉

**Default Route:** Currently set to `/Home/Index`
**To change to PM Dashboard:** Update `Program.cs` default route to `Program/Dashboard`
