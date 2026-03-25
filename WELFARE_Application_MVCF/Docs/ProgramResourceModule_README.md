# WelfareLink - Program & Resource Management Module

## 📋 Module Overview

This module allows Program Managers and Administrators to create, configure, and oversee welfare programmes. It tracks resource allocation (funds and materials) against each programme, monitors budget utilisation, and provides performance insights.

## ✅ What Has Been Implemented

### 1. **Enhanced Service Layer with Business Logic**

#### ProgramService (`Services/ProgramService.cs`)
- **Validation Rules:**
  - End date must be after start date
  - Start date cannot be in the past
  - Budget must be greater than zero
  - Programme titles must be unique
  - Active programmes cannot be deleted (must be suspended/completed first)
- **Auto-Status Management:** New programmes automatically set to "Active" status

#### ResourceService (`Services/ResourceService.cs`)
- **Validation Rules:**
  - Programme must exist and be Active
  - Resource quantity must be greater than zero
  - Total allocated funds cannot exceed programme budget
  - Prevents resource allocation to non-active programmes
- **Budget Control:** Validates resource allocation against programme budget

### 2. **View Models** (`ViewModels/`)
- `ProgramDetailViewModel` - Detailed programme view with resource summary
- `BudgetMonitoringViewModel` & `BudgetDashboardViewModel` - Budget tracking
- `ProgramPerformanceViewModel` - KPI metrics
- `ResourceUtilisationViewModel` - Resource consumption reports

### 3. **Controllers**

#### ProgramController (`Controllers/ProgramController.cs`)
**Actions:**
- `Index` - List all programmes
- `Create` - Create new programme (GET/POST)
- `Edit` - Update programme (GET/POST)
- `Delete` - Delete programme (GET/POST)
- `Details` - View programme details with resource summary
- `BudgetMonitoring` - Dashboard for budget tracking
- `Performance` - Dashboard for programme KPIs

#### ResourceController (`Controllers/ResourceController.cs`)
**Actions:**
- `Index` - List all resources
- `Create` - Allocate new resource (GET/POST)
- `Edit` - Update resource (GET/POST)
- `Details` - View resource details
- `ManageResources` - Manage resources by programme
- `UtilisationReport` - Detailed resource consumption report

### 4. **View Pages** (Bootstrap 5 + Bootstrap Icons)

#### Programme Views (`Views/Program/`)
1. **Index.cshtml** - Programme List
   - Displays all programmes with status badges
   - Quick actions (View, Manage Resources, Edit, Delete)
   - Links to dashboards
   - Color-coded status indicators

2. **Create.cshtml** - Programme Creation
   - Form with validation
   - Date picker for start/end dates
   - Budget input with currency symbol
   - Business rule notifications

3. **Details.cshtml** - Programme Detail Page
   - Complete programme information
   - Budget utilisation gauge
   - Critical budget warning (≥80%)
   - Resource summary cards
   - Allocated resources table
   - Quick navigation to resource management

4. **Edit.cshtml** - Programme Edit
   - Pre-populated form
   - Status dropdown (Active/Completed/Suspended)
   - Validation messages

5. **Delete.cshtml** - Programme Delete Confirmation
   - Warning message
   - Programme summary display
   - Error handling for active programmes

6. **BudgetMonitoring.cshtml** - Budget Dashboard
   - Summary cards (Total Budget, Allocated, Remaining, Critical Programmes)
   - Detailed table with progress bars
   - Visual indicators for critical programmes (≥80%)
   - Budget utilisation percentage

7. **Performance.cshtml** - Performance Dashboard
   - KPI summary cards
   - Application statistics (Total, Approved, Rejected, Pending)
   - Approval rates
   - Benefits disbursed
   - Citizen reach metrics

#### Resource Views (`Views/Resource/`)
1. **Index.cshtml** - Resource List
   - All resources with programme info
   - Type badges (Funds/Materials)
   - Status indicators
   - Quick edit/view actions

2. **Create.cshtml** - Allocate Resource
   - Programme dropdown (Active only)
   - Resource type selector (Funds/Materials)
   - Quantity input with currency
   - Validation rules display
   - Programme budget reference

3. **Edit.cshtml** - Edit Resource
   - Update quantity and status
   - Programme field locked (cannot change after creation)
   - Status dropdown (Available/Reserved/Depleted)

4. **Details.cshtml** - Resource Details
   - Complete resource information
   - Programme details
   - Large quantity display
   - Status badges

5. **ManageResources.cshtml** - Programme Resource Management
   - Breadcrumb navigation
   - Budget summary cards (Total, Allocated, Remaining, Utilisation %)
   - Critical warning alert (≥80%)
   - Resources table by programme
   - Total funds allocated footer
   - Quick resource allocation

6. **UtilisationReport.cshtml** - Resource Utilisation Report
   - Printable report
   - Initial, Used, Remaining quantities
   - Utilisation progress bars
   - Color-coded status (Green < 50%, Yellow 50-80%, Red ≥80%)
   - Export/Print functionality

## 🎨 UI Features

### Design Elements:
- **Bootstrap 5** for responsive layouts
- **Bootstrap Icons** for visual consistency
- **Color Coding:**
  - 🟢 Green: Active, Available, Success
  - 🔴 Red: Critical, Depleted, Danger
  - 🟡 Yellow: Warning, Reserved, Pending
  - 🔵 Blue: Info, Secondary actions
- **Progress Bars** for budget utilisation
- **Badge System** for status indicators
- **Responsive Cards** for summary metrics
- **Alert Messages** for success/error feedback
- **Breadcrumb Navigation** for multi-level pages

## 🔒 Business Rules Implemented

### Programme Rules:
1. End date > Start date
2. Start date ≥ Today
3. Budget > 0
4. Unique programme titles
5. Cannot delete Active programmes
6. Auto-status = "Active" on creation

### Resource Rules:
1. Only allocate to Active programmes
2. Quantity > 0
3. Total Funds ≤ Programme Budget
4. Programme must exist
5. Auto-status = "Available" on creation
6. Budget critical warning at 80%

## 📊 Key Features

### Budget Monitoring:
- Real-time budget tracking
- Utilisation percentage calculation
- Critical threshold alerts (80%)
- Remaining budget calculations
- Visual progress indicators

### Resource Management:
- Programme-wise resource allocation
- Budget validation before allocation
- Status tracking (Available/Reserved/Depleted)
- Type segregation (Funds/Materials)
- Utilisation reporting

### Performance Metrics:
- Programme status dashboard
- Application statistics (placeholders for integration)
- Benefit distribution tracking
- Citizen reach metrics

## 🔄 Navigation Flow

```
Programme List (Index)
├─> Create Programme
├─> Edit Programme
├─> Delete Programme
├─> Programme Details
│   ├─> Resource Management
│   │   ├─> Allocate Resource
│   │   └─> Edit Resource
│   └─> Back to List
├─> Budget Monitoring Dashboard
└─> Performance Dashboard
```

## 🚀 Getting Started

### Prerequisites:
- .NET 10
- SQL Server
- Entity Framework Core

### Setup:
1. Ensure database migrations are applied
2. Run the application
3. Navigate to `/Program` to access Programme List
4. Create programmes and allocate resources

### URL Routes:
- **Programmes:** `/Program/Index`
- **Create Programme:** `/Program/Create`
- **Budget Dashboard:** `/Program/BudgetMonitoring`
- **Performance Dashboard:** `/Program/Performance`
- **Resources:** `/Resource/Index`
- **Manage Resources:** `/Resource/ManageResources?programId={id}`
- **Utilisation Report:** `/Resource/UtilisationReport`

## 🎯 Next Steps (Future Enhancements)

1. **Integration with Application Module:**
   - Link applications to programmes
   - Calculate real approval rates
   - Track application counts

2. **Integration with Benefit Module:**
   - Track disbursed funds
   - Calculate actual resource consumption
   - Update resource status automatically

3. **Notifications:**
   - Alert when budget reaches 80%
   - Notify on programme end date approaching
   - Alert on resource depletion

4. **Audit Logging:**
   - Track all programme changes
   - Resource allocation history
   - Compliance reporting

5. **Reports:**
   - PDF export
   - Excel export
   - Custom date ranges
   - Government audit reports

## 📝 Notes

- All currency values display in ₹ (Rupees)
- Dates formatted as dd-MMM-yyyy
- Validation errors displayed inline
- Success messages via TempData
- Bootstrap Icons CDN used for icons
- Print-friendly utilisation report

## 🛠️ Technologies Used

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core
- Bootstrap 5
- Bootstrap Icons
- Razor Views
- ViewModels/DTOs
- Dependency Injection
- Repository Pattern
- Service Layer Pattern

---

**Module Status:** ✅ Complete and Ready for Testing

**Created By:** Program & Resource Management Module Implementation
**Version:** 1.0
**Date:** 2026
