function chooseNavBar() {
    let str = "";
    switch (localStorage['Job_Id']) {
        case "1"://תורן
            str += ` <li class="active"><a href="Main.html">ראשי</a></li>
                     <li><a href="Requests.html">הגשת סידור</a></li>
                     <li><a href="My_Shifts.html">הסידור שלי</a></li>
                     <li><a href="MyProfile.html">פרטים אישיים</a></li>`; break;
        default:
            str = `<li class="active"><a href="Main.html">ראשי</a></li>
            <li class="dropdown">
                <a href="#">ניהול משמרות<span class="caret"></span></a>
                <ul class="dropdown-menu">
                   <li><a href="AssignToShifts.html">שיבוץ משמרות</a></li>
                   <li><a href="CurrentShifts.html">סידור משמרות נוכחי</a></li>
                </ul>
            </li>
            <li><a href="Insert_Courses.html">הכנסת הכשרות</a></li>
            <li><a href="InsertMessages.html">הכנסת הודעות</a></li>
            <li><a href="SubstitutionRequests.html">בקשות לחילוף</a></li>
            <li><a href="Special_Requirements.html">דרישות משתנות</a></li>
            <li><a href="MyProfile.html">פרטים אישיים</a></li>`; break;
    }
    return str;
}
