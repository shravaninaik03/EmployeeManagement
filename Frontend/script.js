const API_BASE = "http://localhost:5273/api";


/* =========================================================
   SECTION NAVIGATION
========================================================= */

function showSection(sectionId) {

    document.querySelectorAll(".section").forEach(section => {
        section.classList.add("hidden");
    });

    document.getElementById(sectionId).classList.remove("hidden");

    if (sectionId === "dashboardSection") {
        loadDashboard();
    }

    if (sectionId === "employeesSection") {
        loadEmployees();
    }

    if (sectionId === "departmentsSection") {
        loadDepartments();
    }
}


/* =========================================================
   DASHBOARD
========================================================= */

async function loadDashboard() {

    try {

        const response =
            await fetch(`${API_BASE}/Dashboard`);

        if (!response.ok) {
            throw new Error("Failed to fetch dashboard");
        }

        const data = await response.json();

        console.log("Dashboard:", data);
        document.getElementById("totalEmployees").innerText =
        data.totalEmployees;

        document.getElementById("activeEmployees").innerText =
        data.activeEmployees;

        document.getElementById("inactiveEmployees").innerText =
        data.inactiveEmployees;

    document.getElementById("totalDepartments").innerText =
    data.totaldDepartments;

    document.getElementById("joinedThisMonth").innerText =
    data.joinedthismonth;

    }
    catch (error) {

        console.error("Dashboard error:", error);

    }

}


/* =========================================================
   EMPLOYEES
========================================================= */

async function loadEmployees() {

    try {

        const response =
            await fetch(`${API_BASE}/Employee`);

        if (!response.ok) {
            throw new Error("Failed to fetch employees");
        }

        const employees = await response.json();

        const tableBody =
            document.getElementById("employeeTableBody");

        tableBody.innerHTML = "";

        employees.forEach(employee => {

            const row = document.createElement("tr");

            const departmentName =
                employee.department?.departmentName
                || "Department " + employee.departmentId;

            row.innerHTML = `
                <td>${employee.employeeId}</td>

                <td>${employee.empcode}</td>

                <td>
                    ${employee.empFname}
                    ${employee.empLname}
                </td>

                <td>${employee.empemail}</td>

                <td>${departmentName}</td>

                <td>₹${employee.salary}</td>

                <td>
                    ${employee.isActive ? "Active" : "Inactive"}
                </td>

                <td>
                    <div class="actions">

                        <button
                            class="edit-btn"
                            onclick="editEmployee(${employee.employeeId})">
                            Edit
                        </button>

                        <button
                            class="delete-btn"
                            onclick="deleteEmployee(${employee.employeeId})">
                            Delete
                        </button>

                    </div>
                </td>
            `;

            tableBody.appendChild(row);

        });

    }
    catch (error) {

        console.error("Employee loading error:", error);

    }

}
async function searchEmployees() {

    const fname = document.getElementById("searchName").value;
    const deptid = document.getElementById("searchDept").value;
    const lname = document.getElementById("searchLname").value;

    let url = `${API_BASE}/Employee/Search?`;

    if (fname)
        url += `fname=${encodeURIComponent(fname)}&`;

    if (deptid)
        url += `deptid=${deptid}&`;

    if (lname)
        url += `lname=${encodeURIComponent(lname)}`;

    try {

        const response = await fetch(url);

        if (!response.ok) {
            alert("Employee not found");
            return;
        }

        const employees = await response.json();

        const tableBody =
            document.getElementById("employeeTableBody");

        tableBody.innerHTML = "";

        employees.forEach(employee => {

            const row = document.createElement("tr");

            const departmentName =
                employee.department?.departmentName
                || "Department " + employee.departmentId;

            row.innerHTML = `
                <td>${employee.employeeId}</td>
                <td>${employee.empcode}</td>
                <td>${employee.empFname} ${employee.empLname}</td>
                <td>${employee.empemail}</td>
                <td>${departmentName}</td>
                <td>₹${employee.salary}</td>
                <td>${employee.isActive ? "Active" : "Inactive"}</td>
                <td>
                    <div class="actions">
                        <button
                            class="edit-btn"
                            onclick="editEmployee(${employee.employeeId})">
                            Edit
                        </button>

                        <button
                            class="delete-btn"
                            onclick="deleteEmployee(${employee.employeeId})">
                            Delete
                        </button>
                    </div>
                </td>
            `;

            tableBody.appendChild(row);
        });

    }
    catch (error) {
        console.error("Search error:", error);
        alert("Could not search employees.");
    }
}


/* =========================================================
   LOAD DEPARTMENTS
========================================================= */

async function loadDepartments() {

    try {

        const response =
            await fetch(`${API_BASE}/Department`);

        if (!response.ok) {
            throw new Error("Failed to fetch departments");
        }

        const departments = await response.json();

        /* TABLE */

        const tableBody =
            document.getElementById("departmentTableBody");

        tableBody.innerHTML = "";

        departments.forEach(department => {

            const row = document.createElement("tr");

            row.innerHTML = `
                <td>${department.departmentId}</td>

                <td>${department.departmentName}</td>

                <td>

                    <div class="actions">

                        <button
                            class="edit-btn"
                            onclick="editDepartment(${department.departmentId})">
                            Edit
                        </button>

                        <button
                            class="delete-btn"
                            onclick="deleteDepartment(${department.departmentId})">
                            Delete
                        </button>

                    </div>

                </td>
            `;

            tableBody.appendChild(row);

        });


        /* EMPLOYEE DEPARTMENT DROPDOWN */

        const select =
            document.getElementById("departmentId");

        select.innerHTML =
            `<option value="">Select Department</option>`;

        departments.forEach(department => {

            const option =
                document.createElement("option");

            option.value = department.departmentId;

            option.textContent = department.departmentName;

            select.appendChild(option);

        });

    }
    catch (error) {

        console.error("Department loading error:", error);

    }

}


/* =========================================================
   EMPLOYEE MODAL
========================================================= */

function openEmployeeModal(employee = null) {

    document.getElementById("employeeModal").style.display =
        "flex";

    loadDepartments();

    if (employee) {

        document.getElementById("employeeModalTitle")
            .textContent = "Edit Employee";

        document.getElementById("employeeId").value =
            employee.employeeId;

        document.getElementById("empcode").value =
            employee.empcode;

        document.getElementById("empFname").value =
            employee.empFname;

        document.getElementById("empLname").value =
            employee.empLname;

        document.getElementById("empemail").value =
            employee.empemail;

        document.getElementById("empmobile").value =
            employee.empmobile;

        document.getElementById("dob").value =
            employee.dob;

        document.getElementById("departmentId").value =
            employee.departmentId;

        document.getElementById("salary").value =
            employee.salary;

        document.getElementById("joiningDate").value =
            employee.joiningDate;

        document.getElementById("isActive").checked =
            employee.isActive;

    }
    else {

        document.getElementById("employeeModalTitle")
            .textContent = "Add Employee";

        document.getElementById("employeeForm").reset();

        document.getElementById("employeeId").value = "";

    }

}


function closeEmployeeModal() {

    document.getElementById("employeeModal").style.display =
        "none";

}


/* =========================================================
   ADD / UPDATE EMPLOYEE
========================================================= */

document.getElementById("employeeForm")
    .addEventListener("submit", async function (event) {

        event.preventDefault();

        const employeeId =
            document.getElementById("employeeId").value;

        const employee = {

            employeeId:
                employeeId
                    ? parseInt(employeeId)
                    : 0,

            empcode:
                document.getElementById("empcode").value,

            empFname:
                document.getElementById("empFname").value,

            empLname:
                document.getElementById("empLname").value,

            empemail:
                document.getElementById("empemail").value,

            empmobile:
                parseInt(
                    document.getElementById("empmobile").value
                ),

            dob:
                document.getElementById("dob").value,

            departmentId:
                parseInt(
                    document.getElementById("departmentId").value
                ),

            salary:
                parseFloat(
                    document.getElementById("salary").value
                ),

            joiningDate:
                document.getElementById("joiningDate").value,

            isActive:
                document.getElementById("isActive").checked,

            department: null
        };


        try {

            let response;

            if (employeeId) {

                response = await fetch(
                    `${API_BASE}/Employee/${employeeId}`,
                    {
                        method: "PUT",

                        headers: {
                            "Content-Type": "application/json"
                        },

                        body: JSON.stringify(employee)
                    }
                );

            }
            else {

                response = await fetch(
                    `${API_BASE}/Employee`,
                    {
                        method: "POST",

                        headers: {
                            "Content-Type": "application/json"
                        },

                        body: JSON.stringify(employee)
                    }
                );

            }


            if (!response.ok) {

                const errorText =
                    await response.text();

                throw new Error(errorText);

            }


            closeEmployeeModal();

            await loadEmployees();

            await loadDashboard();

        }
        catch (error) {

            console.error(
                "Employee save error:",
                error
            );

            alert(
                "Could not save employee."
            );

        }

    });


/* =========================================================
   EDIT EMPLOYEE
========================================================= */

async function editEmployee(id) {

    try {

        /*
           Your current backend does not have
           GET /api/Employee/{id}.

           So we get all employees and find the one.
        */

        const response =
            await fetch(`${API_BASE}/Employee`);

        const employees =
            await response.json();

        const employee =
            employees.find(
                e => e.employeeId === id
            );

        if (!employee) {

            alert("Employee not found");

            return;

        }

        openEmployeeModal(employee);

    }
    catch (error) {

        console.error(
            "Edit employee error:",
            error
        );

    }

}


/* =========================================================
   DELETE EMPLOYEE
========================================================= */

async function deleteEmployee(id) {

    const confirmed =
        confirm(
            "Are you sure you want to delete this employee?"
        );

    if (!confirmed) {
        return;
    }

    try {

        const response =
            await fetch(
                `${API_BASE}/Employee/${id}`,
                {
                    method: "DELETE"
                }
            );

        if (!response.ok) {
            throw new Error("Delete failed");
        }

        await loadEmployees();

        await loadDashboard();

    }
    catch (error) {

        console.error(
            "Delete employee error:",
            error
        );

        alert(
            "Could not delete employee."
        );

    }

}


/* =========================================================
   DEPARTMENT MODAL
========================================================= */

function openDepartmentModal(department = null) {

    document.getElementById("departmentModal")
        .style.display = "flex";

    if (department) {

        document.getElementById("departmentModalTitle")
            .textContent = "Edit Department";

        document.getElementById("departmentId")
            .value = department.departmentId;

        document.getElementById("departmentName")
            .value = department.departmentName;

    }
    else {

        document.getElementById("departmentModalTitle")
            .textContent = "Add Department";

        document.getElementById("departmentForm")
            .reset();

        document.getElementById("departmentId")
            .value = "";

    }

}


function closeDepartmentModal() {

    document.getElementById("departmentModal")
        .style.display = "none";

}


/* =========================================================
   ADD / UPDATE DEPARTMENT
========================================================= */

document.getElementById("departmentForm")
    .addEventListener("submit", async function (event) {

        event.preventDefault();

        const departmentId =
            document.getElementById("departmentId").value;

        const department = {

            departmentId:
                departmentId
                    ? parseInt(departmentId)
                    : 0,

            departmentName:
                document.getElementById("departmentName").value,

            employees: []

        };


        try {

            let response;

            if (departmentId) {

                response = await fetch(
                    `${API_BASE}/Department/${departmentId}`,
                    {
                        method: "PUT",

                        headers: {
                            "Content-Type": "application/json"
                        },

                        body: JSON.stringify(department)
                    }
                );

            }
            else {

                response = await fetch(
                    `${API_BASE}/Department`,
                    {
                        method: "POST",

                        headers: {
                            "Content-Type": "application/json"
                        },

                        body: JSON.stringify(department)
                    }
                );

            }


            if (!response.ok) {

                const errorText =
                    await response.text();

                throw new Error(errorText);

            }

            closeDepartmentModal();

            await loadDepartments();

            await loadDashboard();

        }
        catch (error) {

            console.error(
                "Department save error:",
                error
            );

            alert(
                "Could not save department."
            );

        }

    });


/* =========================================================
   EDIT DEPARTMENT
========================================================= */

async function editDepartment(id) {

    try {

        const response =
            await fetch(`${API_BASE}/Department`);

        const departments =
            await response.json();

        const department =
            departments.find(
                d => d.departmentId === id
            );

        if (!department) {

            alert("Department not found");

            return;

        }

        openDepartmentModal(department);

    }
    catch (error) {

        console.error(
            "Edit department error:",
            error
        );

    }

}


/* =========================================================
   DELETE DEPARTMENT
========================================================= */

async function deleteDepartment(id) {

    const confirmed =
        confirm(
            "Are you sure you want to delete this department?"
        );

    if (!confirmed) {
        return;
    }

    try {

        const response =
            await fetch(
                `${API_BASE}/Department/${id}`,
                {
                    method: "DELETE"
                }
            );

        if (!response.ok) {

            const errorText =
                await response.text();

            throw new Error(errorText);

        }

        await loadDepartments();

        await loadEmployees();

        await loadDashboard();

    }
    catch (error) {

        console.error(
            "Delete department error:",
            error
        );

        alert(
            "Could not delete department."
        );

    }

}


/* =========================================================
   INITIAL LOAD
========================================================= */

document.addEventListener(
    "DOMContentLoaded",
    async function () {

        await loadDepartments();

        await loadEmployees();

        await loadDashboard();

    }
);