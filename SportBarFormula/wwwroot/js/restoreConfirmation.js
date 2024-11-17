function confirmRestore(url) {
    var modal = createConfirmationModal(url);
    document.body.appendChild(modal);
    var modalInstance = new bootstrap.Modal(modal);
    modalInstance.show();
}

function createConfirmationModal(url) {
    var modal = document.createElement("div");
    modal.className = "modal fade";
    modal.tabIndex = -1;
    modal.setAttribute("aria-labelledby", "confirmationModalLabel");
    modal.setAttribute("aria-hidden", "true");

    var modalDialog = document.createElement("div");
    modalDialog.className = "modal-dialog";

    var modalContent = document.createElement("div");
    modalContent.className = "modal-content custom-modal-content";

    var modalHeader = document.createElement("div");
    modalHeader.className = "modal-header custom-modal-header";
    modalHeader.innerHTML = `
        <h5 class="modal-title" id="confirmationModalLabel">Потвърждение</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    `;

    var modalBody = document.createElement("div");
    modalBody.className = "modal-body custom-modal-body";
    modalBody.textContent = "Сигурни ли сте, че искате да възстановите този артикул?";

    var modalFooter = document.createElement("div");
    modalFooter.className = "modal-footer custom-modal-footer";
    modalFooter.innerHTML = `
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Не</button>
        <button type="button" class="btn btn-primary" onclick="window.location.href='${url}'">Да</button>
    `;

    modalContent.appendChild(modalHeader);
    modalContent.appendChild(modalBody);
    modalContent.appendChild(modalFooter);
    modalDialog.appendChild(modalContent);
    modal.appendChild(modalDialog);

    return modal;
}
