function ControlHideShow(enter, cancel, save, firstSelector, secondSelector, firstDisplayType, secondDisplayType, saveAction) {
    let enterPageBtn = document.getElementById(enter);
    let cancelBtn = document.getElementById(cancel);
    let saveBtn = document.getElementById(save);
    enterPageBtn.addEventListener('click', () => {
        HideShowComponent(firstSelector, secondSelector, secondDisplayType)
    })

    cancelBtn.addEventListener('click', () => {
        HideShowComponent(secondSelector, firstSelector, firstDisplayType)
    })

    if (saveAction) {
        saveBtn.addEventListener('click', () => {
            HideShowComponent(secondSelector, firstSelector, firstDisplayType)
        })
    }
}

function HideShowComponent(hideSelector, showSelector, displayType) {
    let hideDOM = document.querySelector(hideSelector);
    let showDOM = document.querySelector(showSelector);
    hideDOM.style.display = 'none';
    showDOM.style.display = displayType;
}

ControlHideShow("name-edit", "cancel-name-edit-btn", "save-name-edit-btn", "div.name-component", "div.name-edit-component", "flex", "block", true);
ControlHideShow("verify-email", "cancel-verify-email-btn", "sent-verify-email-btn", "div.email-component", "div.verify-email-component", "block", "block", true);

ControlHideShow("phone-edit", "cancel-phone-btn", "save-phone-btn", "div.phone-component", "div.phone-edit-component", "flex", "block", true);
ControlHideShow("password-edit", "cancel-password-btn", "save-password-btn", "div.password-component", "div.password-edit-component", "flex", "block", true);
