function chooseNavBar() {
    let str = "";
    switch (localStorage['Job_Id']) {
        case "1"://תורן
            str += ` <li class="active"><a href="#">ראשי</a></li>
                            <li class="dropdown">
                                <a href="#">סידור עבודה<span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="Requests.html">הגשת סידור</a></li>
                                    <li><a href="My_Shifts.html">הסידור שלי</a></li>
                                </ul>
                            </li>
                            <li><a href="#">נוכחות</a></li>
                            <li><a href="#">אנשי קשר</a></li>
                            <li class="dropdown">
                                <a href="#">ניהול תורנות<span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#">דוח סוף תורנות</a></li>
                                    <li><a href="#">טופס עליה לתורנות</a></li>
                                </ul>
                            </li>
                            <li class="dropdown">
                                <a href="#">הכשרות ואישורים<span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#">ההכשרות שלי</a></li>
                                    <li><a href="#">בקשה להכשרה</a></li>
                                    <li><a href="Insert_Courses.html">הכנסת הכשרות</a></li>
                                </ul>
                            </li>
                            <li><a href="#">פרטים אישיים</a></li>`; break;
        default:
            str = `<li class="active"><a href="#">ראשי</a></li>
            <li class="dropdown">
                <a href="#">סידור עבודה<span class="caret"></span></a>
                <ul class="dropdown-menu">
                    <li><a href="Requests.html">הגשת סידור</a></li>
                    <li><a href="My_Shifts.html">הסידור שלי</a></li>
                    <li><a href="AssignToShifts.html">שיבוץ בקשות</a></li>
                </ul>
            </li>
            <li><a href="#">נוכחות</a></li>
            <li><a href="#">אנשי קשר</a></li>
            <li class="dropdown">
                <a href="#">ניהול תורנות<span class="caret"></span></a>
                <ul class="dropdown-menu">
                    <li><a href="#">דוח סוף תורנות</a></li>
                    <li><a href="#">טופס עליה לתורנות</a></li>
                </ul>
            </li>
            <li class="dropdown">
                <a href="#">הכשרות ואישורים<span class="caret"></span></a>
                <ul class="dropdown-menu">
                    <li><a href="#">ההכשרות שלי</a></li>
                    <li><a href="#">בקשה להכשרה</a></li>
                    <li><a href="Insert_Courses.html">הכנסת הכשרות</a></li>
                </ul>
            </li>
            <li><a href="#">פרטים אישיים</a></li>
            <li><a href="Special_Requirements.html">דרישות משתנות</a></li>`; break;
    }
    return str;
}
