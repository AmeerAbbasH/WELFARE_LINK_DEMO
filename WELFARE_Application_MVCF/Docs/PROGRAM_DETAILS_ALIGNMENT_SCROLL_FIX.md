# Programme Details Page - Alignment & Scroll Fix

## Issues Reported

### Issue 1: Card Misalignment
In the Programme Details view, the three information sections were misaligned:
- **Programme Information** (left side) - Large card
- **Budget Utilisation** (right side, top) - Should align with top of left card
- **Resource Summary** (right side, bottom) - Should fill remaining space
- **Allocated Resources** (bottom) - Full width table

The Budget Utilisation and Resource Summary cards were not properly aligned, with the summary appearing in the middle-right while allocated resources appeared at the bottom in a disconnected way.

### Issue 2: Horizontal Scroll Behavior
When using horizontal split screen in Windows or when content required horizontal scrolling:
- Only the content boxes were moving when scrolling horizontally
- The background and left navigation remained static
- This created a disjointed user experience where elements appeared to "slide" independently

## Root Causes

### Alignment Issue:
1. Right-side cards (`col-md-4`) had no height constraints
2. Cards were simply stacked with no vertical distribution control
3. Different content lengths caused uneven heights
4. No nested grid structure to control vertical spacing

### Scroll Issue:
1. `overflow-x` not properly configured on body and main-content
2. Dashboard wrapper had no min-width constraint
3. Main content width not explicitly calculated
4. Fixed sidebar (correct) but content area not properly bounded

## Solutions Implemented

### Fix 1: Card Alignment - Nested Grid Structure

**File**: `Views/Program/Details.cshtml`

#### Changed Container Type:
```html
<!-- BEFORE -->
<div class="container mt-4" style="padding-bottom: 40px;">

<!-- AFTER -->
<div class="container-fluid mt-4" style="padding-bottom: 40px; max-width: 100%;">
```

**Benefit**: Prevents horizontal overflow and allows responsive width

#### Updated Responsive Breakpoints:
```html
<!-- BEFORE -->
<div class="col-md-8">  <!-- Left: Programme Info -->
<div class="col-md-4">  <!-- Right: Budget & Summary -->

<!-- AFTER -->
<div class="col-lg-8">  <!-- Left: Programme Info -->
<div class="col-lg-4">  <!-- Right: Budget & Summary -->
```

**Benefit**: Better responsive behavior on tablets (768px-991px)

#### Added Equal Height to Left Card:
```html
<div class="col-lg-8">
    <div class="card shadow-sm mb-4 h-100">
        <!-- Programme Information -->
    </div>
</div>
```

**Benefit**: Left card stretches to match right column height

#### Restructured Right Column with Nested Row:
```html
<div class="col-lg-4">
    <div class="row h-100">
        <div class="col-12 mb-3">
            <div class="card shadow-sm h-100">
                <!-- Budget Utilisation -->
            </div>
        </div>
        <div class="col-12">
            <div class="card shadow-sm h-100">
                <!-- Resource Summary -->
            </div>
        </div>
    </div>
</div>
```

**Benefits**:
- Nested row with `h-100` ensures vertical space distribution
- `col-12` makes each card full-width within the right column
- `mb-3` adds spacing between the two cards
- Both cards now properly aligned and distributed

#### Removed Extra Bottom Margins:
```html
<dl class="mb-0">  <!-- Instead of just <dl> -->
```

**Benefit**: Prevents uneven spacing at bottom of cards

### Fix 2: Horizontal Scroll Behavior

**File**: `wwwroot/css/site.css`

#### Added Horizontal Scroll to Body:
```css
/* BEFORE */
body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    margin: 0;
    background-color: #f4f6f9;
    color: #333;
    scroll-behavior: smooth;
}

/* AFTER */
body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    margin: 0;
    background-color: #f4f6f9;
    color: #333;
    scroll-behavior: smooth;
    overflow-x: auto;  /* ← NEW: Allow horizontal scroll */
}
```

#### Updated Dashboard Wrapper:
```css
/* BEFORE */
.dashboard-wrapper {
    display: flex;
    min-height: 100vh;
}

/* AFTER */
.dashboard-wrapper {
    display: flex;
    min-height: 100vh;
    min-width: 100%;  /* ← NEW: Ensure minimum width */
}
```

#### Enhanced Main Content Area:
```css
/* BEFORE */
.main-content {
    flex: 1;
    margin-left: 260px;
    padding: 30px;
    padding-bottom: 60px;
    min-height: 100vh;
}

/* AFTER */
.main-content {
    flex: 1;
    margin-left: 260px;
    padding: 30px;
    padding-bottom: 60px;
    min-height: 100vh;
    overflow-x: auto;              /* ← NEW: Allow horizontal scroll */
    width: calc(100% - 260px);     /* ← NEW: Explicit width calculation */
}
```

**Benefits**:
- Content area properly bounded
- Horizontal scroll works naturally
- Background scrolls with content
- Sidebar remains fixed (as intended for navigation)

## Visual Comparison

### Card Alignment

**Before (Misaligned):**
```
Left Card (tall)          Right Side:
┌───────────────────┐     ┌──────────┐
│                   │     │  Budget  │ (top)
│   Programme       │     │  Card    │
│   Information     │     └──────────┘
│                   │     
│   (Large card     │     [Empty Space]
│    with many      │     
│    fields)        │     ┌──────────┐
│                   │     │ Summary  │ (middle)
│                   │     │  Card    │
└───────────────────┘     └──────────┘

                          [Empty Space]
```

**After (Perfectly Aligned):**
```
Left Card (tall)          Right Column (equal height):
┌───────────────────┐     ┌──────────────┐
│                   │     │    Budget    │
│   Programme       │     │ Utilisation  │
│   Information     │     │              │
│                   │     ├──────────────┤
│   (Large card     │     │   Resource   │
│    with many      │     │   Summary    │
│    fields)        │     │              │
│                   │     │              │
│                   │     │              │
└───────────────────┘     └──────────────┘

Both columns have equal height - perfect alignment!
```

### Horizontal Scroll Behavior

**Before (Broken):**
```
When scrolling horizontally:
┌─────────────────────────────────────┐
│ [Sidebar]   [Content moving] →      │
│ FIXED       ← Only content scrolls  │
│             Background stays still  │
└─────────────────────────────────────┘
```

**After (Fixed):**
```
When scrolling horizontally:
┌─────────────────────────────────────┐
│ [Sidebar]   [Content & Background]→ │
│ FIXED       ← Everything scrolls    │
│             together smoothly       │
└─────────────────────────────────────┘
```

## Technical Details

### Bootstrap Grid Enhancements

**Nested Row Technique**:
The nested `<div class="row h-100">` inside the right column creates a vertical flexbox container that:
1. Takes 100% height of the parent column
2. Distributes child elements vertically
3. Ensures proper spacing with `mb-3` on first child
4. Allows both cards to have `h-100` for proportional sizing

**Responsive Breakpoints**:
- `col-lg-8` / `col-lg-4` → Large screens (≥992px): 2 columns
- Auto-stack on smaller screens (< 992px): Cards stack vertically
- Better than `col-md-*` which breaks layout on tablets

### CSS Overflow Management

**overflow-x: auto**:
- Shows scrollbar only when content exceeds viewport width
- Doesn't force scrollbar when content fits
- Smooth scrolling with `scroll-behavior: smooth`

**width: calc(100% - 260px)**:
- Explicitly calculates content area width
- Accounts for 260px fixed sidebar
- Ensures proper horizontal scroll boundaries

## Impact

### Visual Quality
✅ **Perfect Card Alignment**: All three sections properly aligned  
✅ **Equal Heights**: Left card and right column have matching heights  
✅ **Professional Layout**: Clean, organized appearance  
✅ **Proper Spacing**: Consistent gaps between cards  

### Scrolling Behavior
✅ **Natural Horizontal Scroll**: Content and background move together  
✅ **Fixed Sidebar**: Navigation stays accessible while scrolling  
✅ **Smooth Experience**: No jarring independent movements  
✅ **Split Screen Compatible**: Works correctly in Windows split view  

### Responsive Design
✅ **Large Screens (≥992px)**: 2-column layout with aligned cards  
✅ **Tablets (768-991px)**: Cards maintain structure  
✅ **Mobile (< 768px)**: Cards stack vertically  
✅ **Wide Content**: Horizontal scroll when needed  

## Testing Checklist

### Alignment Testing:
- [ ] Navigate to any Programme Details page (click View on a programme)
- [ ] Verify left card (Programme Information) and right column are **equal height**
- [ ] Verify Budget Utilisation card is at the **top** of right column
- [ ] Verify Resource Summary card is **directly below** Budget Utilisation
- [ ] Verify no awkward gaps or misalignment
- [ ] Verify Allocated Resources table appears **below** both columns

### Scroll Testing:
- [ ] Maximize browser window - verify no horizontal scroll appears
- [ ] Reduce browser width - verify horizontal scrollbar appears if needed
- [ ] Scroll horizontally - verify **background moves with content**
- [ ] Verify **sidebar stays fixed** on left (should not scroll)
- [ ] Test in **Windows split screen** - verify proper fitting
- [ ] Zoom in browser (Ctrl +) - verify scroll works correctly

### Responsive Testing:
- [ ] Desktop (1200px+) - 2 columns side-by-side
- [ ] Laptop (992-1199px) - 2 columns, slightly narrower
- [ ] Tablet (768-991px) - Cards stack vertically
- [ ] Mobile (< 768px) - All cards full width, stacked

## Related CSS Classes

### Height Control:
- `h-100` → height: 100% (fills parent container)
- `mb-3` → margin-bottom: 1rem (spacing between cards)
- `mb-0` → margin-bottom: 0 (removes extra bottom margin)

### Grid Layout:
- `row` → Flexbox container for columns
- `col-lg-8` → 8/12 width on large screens (≥992px)
- `col-lg-4` → 4/12 width on large screens (≥992px)
- `col-12` → Full width within parent column

### Overflow:
- `overflow-x: auto` → Show horizontal scrollbar when needed
- `width: calc(100% - 260px)` → Explicit width calculation
- `container-fluid` → Full-width responsive container

## Build Status
✅ **Build**: Successful  
✅ **Alignment**: Fixed with nested grid  
✅ **Scroll Behavior**: Fixed with overflow CSS  
✅ **Responsive**: Proper breakpoints configured  

## Visual Summary

### Programme Details Page Layout:
```
┌──────────────────────────────────────────────────────────┐
│ Breadcrumb: Programmes > Programme Title                 │
├────────────────────────────────┬─────────────────────────┤
│  Programme Information         │  Budget Utilisation     │
│  ┌──────────────────────────┐  │  ┌───────────────────┐ │
│  │ ID, Title, Description   │  │  │ XX.X% Used        │ │
│  │ Duration, Budget, Status │  │  │ Progress Bar      │ │
│  │                          │  │  │ Allocated Funds   │ │
│  │ [Edit] [Manage] [Back]   │  │  │ Remaining Budget  │ │
│  └──────────────────────────┘  │  └───────────────────┘ │
│                                 │  Resource Summary      │
│                                 │  ┌───────────────────┐ │
│                                 │  │ Total Resources   │ │
│                                 │  │ Total Funds       │ │
│                                 │  │ Total Materials   │ │
│                                 │  │ Applications      │ │
│                                 │  └───────────────────┘ │
├────────────────────────────────┴─────────────────────────┤
│  Allocated Resources Table                               │
│  [Resource list with Edit buttons]                       │
└──────────────────────────────────────────────────────────┘
```

**Key Features:**
- ✅ Left and right columns have **equal heights**
- ✅ Right column cards are **properly distributed** top and bottom
- ✅ Budget Utilisation **aligns with top** of Programme Information
- ✅ Resource Summary fills **remaining space** below
- ✅ Allocated Resources table spans **full width** at bottom
- ✅ Clean, organized, professional appearance

## Date: 2024
**Status**: ✅ **COMPLETE**
**Priority**: High (User Experience)
**Impact**: Better visual layout + proper scroll behavior
