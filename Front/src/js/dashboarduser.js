
        // Dữ liệu ví dụ

     const eventList = [
        { id: "1", title: "Event 1", date: "2022-01-01", banner: "Banner 1" },
        { id: "2", title: "Event 2", date: "2022-02-01", banner: "Banner 2" },
        { id: "3", title: "Event 3", date: "2022-03-01", banner: "Banner 3" },
        ];  

     const newsList = [
        { id: "1", title: "News 1", date: "2022-01-01" },
        { id: "2", title: "News 2", date: "2022-02-01" },
        { id: "3", title: "News 3", date: "2022-03-01" },
        ];

     const bannerList = [
        { id: "1", image: "banner1.jpg", event: "Event 1" },
        { id: "2", image: "banner2.jpg", event: "Event 2" },
        { id: "3", image: "banner3.jpg", event: "Event 3" },
        ];

     const locationList = [
        { id: "1", coffeeShop: "Coffee Shop 1", location: "Location 1", plusCode: "PlusCode 1" },
        { id: "2", coffeeShop: "Coffee Shop 2", location: "Location 2", plusCode: "PlusCode 2" },
        { id: "3", coffeeShop: "Coffee Shop 3", location: "Location 3", plusCode: "PlusCode 3" },
        ];

     const serviceList = [
        { id: "1", service: "Service 1", coffeeShop: "Coffee Shop 1" },
        { id: "2", service: "Service 2", coffeeShop: "Coffee Shop 2" },
        { id: "3", service: "Service 3", coffeeShop: "Coffee Shop 3" },
        ];

     const customerFollowingList = [
        { id: "1", userName: "User 1", email: "user1@example.com", coffeeShop: "Coffee Shop 1" },
        { id: "2", userName: "User 2", email: "user2@example.com", coffeeShop: "Coffee Shop 2" },
        { id: "3", userName: "User 3", email: "user3@example.com", coffeeShop: "Coffee Shop 3" },
        ];

     function showDashboard() {
            // Hiển thị phần tử chart và data-section
        document.getElementById("chart").style.display = "block";
        document.querySelector(".data-section").style.display = "block";

            // Ẩn phần tử user-list, event-list, news-list, banner-list, location-list, service-list, customer-following-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";
// Cập nhật thông tin UserID và avatar
        const userIdElement = document.getElementById("user-id");
        const userAvatarElement = document.querySelector(".user-avatar img");

    // Thay đổi thông tin UserID và avatar tại đây
        const userId = "123456";
        const userAvatar = "img/avatar.png";

        userIdElement.textContent = userId;
        userAvatarElement.src = userAvatar;

            // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "Dashboard";
    }

    function showUserList(listType) {
        if (listType === "blacklist") {
            document.getElementById("user-list-title").innerText = "Black List User";
        } else if (listType === "customer") {
            document.getElementById("user-list-title").innerText = "List Customer";
        } else if (listType === "request") {
            document.getElementById("user-list-title").innerText = "Request User";
        }

            // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

            // Hiển thị phần tử user-list và ẩn event-list, news-list, banner-list, location-list, service-list, customer-following-list
        document.getElementById("user-list").style.display = "block";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";

            // Hiển thị danh sách người dùng
        const userListContent = document.getElementById("user-list-content");
        userListContent.innerHTML = "";
        userList.forEach((user, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const nameCell = document.createElement("td");
            nameCell.textContent = user.name;
            row.appendChild(nameCell);

            const emailCell = document.createElement("td");
            emailCell.textContent = user.email;
            row.appendChild(emailCell);

            const createdDateCell = document.createElement("td");
            createdDateCell.textContent = user.createdDate;
            row.appendChild(createdDateCell);

            const functionCell = document.createElement("td");

            if (listType === "request") {
                const acceptButton = document.createElement("button");
                acceptButton.textContent = "Accept";
                    // Gán sự kiện onclick cho nút accept
                acceptButton.onclick = function () {
                        // Thực hiện hành động accept
                    acceptRequest(user.id);
                };
                functionCell.appendChild(acceptButton);

                const declineButton = document.createElement("button");
                declineButton.textContent = "Decline";
                    // Gán sự kiện onclick cho nút decline
                declineButton.onclick = function () {
                        // Thực hiện hành động decline
                    declineRequest(user.id);
                };
                functionCell.appendChild(declineButton);
            } else {
                const editButton = document.createElement("button");
                editButton.textContent = "Edit";
                    // Gán sự kiện onclick cho nút sửa
                editButton.onclick = function () {
                        // Thực hiện hành động sửa
                    editUser(user.id);
                };
                functionCell.appendChild(editButton);

                const deleteButton = document.createElement("button");
                deleteButton.textContent = "Delete";
                    // Gán sự kiện onclick cho nút xóa
                deleteButton.onclick = function () {
                        // Thực hiện hành động xóa
                    deleteUser(user.id);
                };
                functionCell.appendChild(deleteButton);
            }

            row.appendChild(functionCell);

            userListContent.appendChild(row);
        });

            // Cập nhật tiêuđề
        document.getElementById("section-title").innerText = "";
    }

    function showEventList() {
    // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

    // Ẩn phần tử user-list, news-list, banner-list, location-list, service-list, customer-following-list và hiển thị event-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";
        document.getElementById("event-list").style.display = "block";

    // Hiển thị danh sách sự kiện
        const eventListContent = document.getElementById("event-list-content");
        eventListContent.innerHTML = "";
        eventList.forEach((event, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const titleCell = document.createElement("td");
            titleCell.textContent = event.title;
            row.appendChild(titleCell);

            const dateCell = document.createElement("td");
            dateCell.textContent = event.date;
            row.appendChild(dateCell);

            const bannerCell = document.createElement("td");
            bannerCell.textContent = event.banner;
            row.appendChild(bannerCell);

            const functionCell = document.createElement("td");

            const createButton = document.createElement("button");
            createButton.textContent = "Create";
        // Gán sự kiện onclick cho nút create
            createButton.onclick = function () {
            // Thực hiện hành động create
                createEvent();
            };
            functionCell.appendChild(createButton);

            const editButton = document.createElement("button");
            editButton.textContent = "Edit";
        // Gán sự kiện onclick cho nút edit
            editButton.onclick = function () {
            // Thực hiện hành động edit
                editEvent(event.id);
            };
            functionCell.appendChild(editButton);

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
        // Gán sự kiện onclick cho nút delete
            deleteButton.onclick = function () {
            // Thực hiện hành động delete
                deleteEvent(event.id);
            };
            functionCell.appendChild(deleteButton);

            row.appendChild(functionCell);

            eventListContent.appendChild(row);
        });

    // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "List Events";
    }


    function showNewsList() {
            // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

            // Ẩn phần tử user-list, event-list, banner-list, location-list, service-list, customer-following-list và hiển thị news-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";
        document.getElementById("news-list").style.display = "block";

            // Hiển thị danh sách news
        const newsListContent = document.getElementById("news-list-content");
        newsListContent.innerHTML = "";
        newsList.forEach((news, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const titleCell = document.createElement("td");
            titleCell.textContent = news.title;
            row.appendChild(titleCell);

            const dateCell = document.createElement("td");
            dateCell.textContent = news.date;
            row.appendChild(dateCell);

            const functionCell = document.createElement("td");

            const createButton = document.createElement("button");
            createButton.textContent = "Create";
                // Gán sự kiện onclick cho nút create
            createButton.onclick = function () {
                    // Thực hiện hành động create
                createNews();
            };
            functionCell.appendChild(createButton);

            const editButton = document.createElement("button");
            editButton.textContent = "Edit";
                // Gán sự kiện onclick cho nút edit
            editButton.onclick = function () {
                    // Thực hiện hành động edit
                editNews(news.id);
            };
            functionCell.appendChild(editButton);

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
                // Gán sự kiện onclick cho nút delete
            deleteButton.onclick = function () {
                    // Thực hiện hành động delete
                deleteNews(news.id);
            };
            functionCell.appendChild(deleteButton);

            row.appendChild(functionCell);

            newsListContent.appendChild(row);
        });

            // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "View List News";
    }

    function showBannerList() {
            // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

            // Ẩn phần tử user-list, event-list, news-list, location-list, service-list, customer-following-list và hiển thị banner-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";
        document.getElementById("banner-list").style.display = "block";

            // Hiển thị danh sách banner
        const bannerListContent = document.getElementById("banner-list-content");
        bannerListContent.innerHTML = "";
        bannerList.forEach((banner, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const imageCell = document.createElement("td");
            imageCell.textContent = banner.image;
            row.appendChild(imageCell);

            const eventCell = document.createElement("td");
            eventCell.textContent = banner.event;
            row.appendChild(eventCell);

            const functionCell = document.createElement("td");

            const createButton = document.createElement("button");
            createButton.textContent = "Create";
                // Gán sự kiện onclick cho nút create
            createButton.onclick = function () {
                    // Thực hiện hành động create
                createBanner();
            };
            functionCell.appendChild(createButton);

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
                // Gán sự kiện onclick cho nút delete
            deleteButton.onclick = function () {
                    // Thực hiện hành động delete
                deleteBanner(banner.id);
            };
            functionCell.appendChild(deleteButton);

            row.appendChild(functionCell);

            bannerListContent.appendChild(row);
        });

            // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "View List Banners";
    }

    function showLocationList() {
            // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

            // Ẩn phần tử user-list, event-list, news-list, banner-list, service-list, customer-following-list và hiển thị location-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";
        document.getElementById("location-list").style.display = "block";

            // Hiển thị danh sách location
        const locationListContent = document.getElementById("location-list-content");
        locationListContent.innerHTML = "";
        locationList.forEach((location, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const coffeeShopCell = document.createElement("td");
            coffeeShopCell.textContent = location.coffeeShop;
            row.appendChild(coffeeShopCell);

            const locationCell = document.createElement("td");
            locationCell.textContent = location.location;
            row.appendChild(locationCell);

            const plusCodeCell = document.createElement("td");
            plusCodeCell.textContent = location.plusCode;
            row.appendChild(plusCodeCell);

            const functionCell = document.createElement("td");

            const createButton = document.createElement("button");
            createButton.textContent = "Create";
                // Gán sự kiện onclick cho nút create
            createButton.onclick = function () {
                    // Thực hiện hành động create
                createLocation();
            };
            functionCell.appendChild(createButton);

            const editButton = document.createElement("button");
            editButton.textContent = "Edit";
                // Gán sự kiện onclick cho nút edit
            editButton.onclick = function () {
                    // Thực hiện hành động edit
                editLocation(location.id);
            };
            functionCell.appendChild(editButton);

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
                // Gán sự kiện onclick cho nút delete
            deleteButton.onclick = function () {
                    // Thực hiện hành động delete
                deleteLocation(location.id);
            };
            functionCell.appendChild(deleteButton);

            row.appendChild(functionCell);

            locationListContent.appendChild(row);
        });

            // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "View List Locations";
    }

    function showServiceList() {
            // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

            // Ẩn phần tử user-list, event-list, news-list, banner-list, location-list, customer-following-list và hiển thị service-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "none";
        document.getElementById("service-list").style.display = "block";

            // Hiển thị danh sách service
        const serviceListContent = document.getElementById("service-list-content");
        serviceListContent.innerHTML = "";
        serviceList.forEach((service, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const serviceCell = document.createElement("td");
            serviceCell.textContent = service.service;
            row.appendChild(serviceCell);

            const coffeeShopCell = document.createElement("td");
            coffeeShopCell.textContent = service.coffeeShop;
            row.appendChild(coffeeShopCell);

            const functionCell = document.createElement("td");

            const createButton = document.createElement("button");
            createButton.textContent = "Create";
                // Gán sự kiện onclick cho nút create
            createButton.onclick = function () {
                    // Thực hiện hành động create
                createService();
            };
            functionCell.appendChild(createButton);

            const editButton = document.createElement("button");
            editButton.textContent = "Edit";
                // Gán sự kiện onclick cho nút edit
            editButton.onclick = function () {
                    // Thực hiện hành động edit
                editService(service.id);
            };
            functionCell.appendChild(editButton);

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
                // Gán sự kiện onclick cho nút delete
            deleteButton.onclick = function () {
                    // Thực hiện hành động delete
                deleteService(service.id);
            };
            functionCell.appendChild(deleteButton);

            row.appendChild(functionCell);

            serviceListContent.appendChild(row);
        });

            // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "View List Services";
    }

    function showCustomerFollowingList() {
            // Ẩn phần tử chart và data-section
        document.getElementById("chart").style.display = "none";
        document.querySelector(".data-section").style.display = "none";

            // Ẩn phần tử user-list, event-list, news-list, banner-list, location-list, service-list và hiển thị customer-following-list
        document.getElementById("user-list").style.display = "none";
        document.getElementById("event-list").style.display = "none";
        document.getElementById("news-list").style.display = "none";
        document.getElementById("banner-list").style.display = "none";
        document.getElementById("location-list").style.display = "none";
        document.getElementById("service-list").style.display = "none";
        document.getElementById("customer-following-list").style.display = "block";

            // Hiển thị danh sách customer following
        const customerFollowingListContent = document.getElementById("customer-following-list-content");
        customerFollowingListContent.innerHTML = "";
        customerFollowingList.forEach((customer, index) => {
            const row = document.createElement("tr");

            const sttCell = document.createElement("td");
            sttCell.textContent = index + 1;
            row.appendChild(sttCell);

            const userNameCell = document.createElement("td");
            userNameCell.textContent = customer.userName;
            row.appendChild(userNameCell);

            const emailCell = document.createElement("td");
            emailCell.textContent = customer.email;
            row.appendChild(emailCell);

            const coffeeShopCell = document.createElement("td");
            coffeeShopCell.textContent = customer.coffeeShop;
            row.appendChild(coffeeShopCell);

            customerFollowingListContent.appendChild(row);
        });

            // Cập nhật tiêu đề
        document.getElementById("section-title").innerText = "View List Customer Following";
    }

    function logout() {
            // Thực hiện các hành động cần thiết khi logout

            // Ví dụ: Chuyển hướng đến trang đăng nhập
        window.location.href = "login.html";
    }
