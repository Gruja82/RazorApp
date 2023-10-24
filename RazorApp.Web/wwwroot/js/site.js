function setSrc() {
    let img = document.getElementById("imgElement");
    const [inputValue] = document.getElementById("inputElement").files;
    if (inputValue) {
        img.src = URL.createObjectURL(inputValue);
    }

}
