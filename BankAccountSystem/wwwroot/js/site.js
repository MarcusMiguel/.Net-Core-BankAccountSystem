$(document).ready(function () {
    $('body').on('click', '.btnModal', function () {
        $('#myModal .modal-title').html("Adicionar");
        $("#teste").load("/Admin/SignUp/" , function () {
            $('#myModal').modal("show");
            $.validator.unobtrusive.parse($("form"));
        });
    });
    $('body').on('click', '.transferButton', function () {
        $(".card").hide();
        $("#card").load("/Transfer/", function () {
            $.validator.unobtrusive.parse($("form"));
        });
    });
    $('body').on('click', '.depositButton', function () {
        $(".card").hide();
        $("#card").load("/Deposit/", function () {
            $.validator.unobtrusive.parse($("form"));
        });
    });
    $('body').on('click', '.withdrawButton', function () {
        $(".card").hide();
        $("#card").load("/Withdraw/", function () {
            $.validator.unobtrusive.parse($("form"));
        });
    });
    $('body').on('click', '.accountButton', function () {
        $(".card").hide();
        $("#accountCard").show();
    });
});

function block_unblock(email) {
    $.ajax({
        url: '/Admin/Block_Unblock/',
        data: { email: email },
        success: function () {
            $("#mydiv").load(location.href + " #mydiv>*", "");
            toastr.success("Operação realizada com sucesso.");
        }
    });
}

// Designed by:  Mauricio Bucardo
"use strict";

const body = document.body;
const bgColorsBody = ["#ffb457", "#ff96bd", "#9999fb", "#ffe797", "#cffff1"];
const menu = body.querySelector(".menu");
const menuItems = menu.querySelectorAll(".menu__item");
const menuBorder = menu.querySelector(".menu__border");
let activeItem = menu.querySelector(".active");

function clickItem(item, index) {

    menu.style.removeProperty("--timeOut");

    if (activeItem == item) return;

    if (activeItem) {
        activeItem.classList.remove("active");
    }


    item.classList.add("active");
    body.style.backgroundColor = bgColorsBody[index];
    activeItem = item;
    offsetMenuBorder(activeItem, menuBorder);


}

function offsetMenuBorder(element, menuBorder) {

    const offsetActiveItem = element.getBoundingClientRect();
    const left = Math.floor(offsetActiveItem.left - menu.offsetLeft - (menuBorder.offsetWidth - offsetActiveItem.width) / 2) + "px";
    menuBorder.style.transform = `translate3d(${left}, 0 , 0)`;

}

offsetMenuBorder(activeItem, menuBorder);

menuItems.forEach((item, index) => {

    item.addEventListener("click", () => clickItem(item, index));

})

window.addEventListener("resize", () => {
    offsetMenuBorder(activeItem, menuBorder);
    menu.style.setProperty("--timeOut", "none");
});


jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: "/Admin/SignUp/",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#myModal .modal-body').html('');
                $('#myModal .modal-title').html('');
                $('#myModal').modal('hide');
                $("#mydiv").load(location.href + " #mydiv>*", "");
                $("#myModal").load(location.href + " #myModal>*", "");
                toastr.success("Operação realizada com sucesso.");
            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
                toastr.success("Ocorreu um erro ao realizar a operação.");
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxTransferPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: "/Transfer/",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if ($(form).valid()) {
                    toastr.success("Operação realizada com sucesso.");
                    $(".accountButton").trigger("click");
                    $(".card").load(location.href + " .card>*", "");
                }
            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
                toastr.success("Ocorreu um erro ao realizar a operação.");
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDepositPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: "/Deposit/",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if ($(form).valid()) {
                    toastr.success("Operação realizada com sucesso.");
                    $(".accountButton").trigger("click");
                    $(".card").load(location.href + " .card>*", "");
                }
            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
                toastr.success("Ocorreu um erro ao realizar a operação.");
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxWithdrawPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: "/Withdraw/",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if ($(form).valid()) {
                    toastr.success("Operação realizada com sucesso.");
                    $(".accountButton").trigger("click");
                    $(".card").load(location.href + " .card>*", "");
                }
            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
                toastr.success("Ocorreu um erro ao realizar a operação.");
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDepositPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: "/Deposit/",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if ($(form).valid()) {
                    toastr.success("Operação realizada com sucesso.");
                    $(".accountButton").trigger("click");
                    $(".card").load(location.href + " .card>*", "");
                }
            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
                toastr.success("Ocorreu um erro ao realizar a operação.");
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}