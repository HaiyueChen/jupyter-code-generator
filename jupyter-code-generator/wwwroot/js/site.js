// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let file_template = `<div class="file">
                        <p class="file-name">
                            {to_be_replaced}
                        </p>
                     </div>`;

let directory_template = `<div class="directory">
                            <button class="btn-sm btn-primary dropdown-toggle" type="button" id="{directory-name}button" data-toggle="collapse" data-target="#{directory-name}" aria-expanded="false" aria-controls="directory-content">{directory-name}</button>
                        
                            <div class="collapse multi-collapse" id="{directory-name}">
                                <div class="card card-body"> 
                                    {folder-content}
                                </div>
                            </div>
                        </div>`

let selected_file_template = `
                    <div class="selected-file">
                        <p class="selected-filename">
                            {to_be_replaced}
                        </p>
                     </div>
    
`

const load_file_structure =
    selectedContainer => {
        if (selectedContainer == "") {
            return 
        }

        let folder_ids = [];
        fetch(`/api/Generator/${selectedContainer}`).then(
            res => res.json()
        ).then(
            json_obj => {
                const roots = json_obj["_roots"];
                console.log(roots)
                for (let i = 0; i < roots.length; i++) {
                    let current_item = roots[i];
                    if (current_item._children.length == 0) {
                        $("#files").append(
                            file_template.replace("{to_be_replaced}", roots[i]._content["Name"])
                        );
                    }
                    else {
                        let folder_name = current_item._content.Prefix.replace("/", "-")
                        folder_ids.push(folder_name)
                        let directory_template_with_name = directory_template.replace(new RegExp("{directory-name}", 'g'), folder_name)
                        let folder_content = generate_folder_tree(current_item._children, folder_ids);
                        let finished_directory_html = directory_template_with_name.replace(new RegExp("{folder-content}", 'g'), folder_content)
                        $("#files").append(
                            finished_directory_html
                        )
                    }
                }
            }
        ).then(
            () => {
                for (var i = 0; i < folder_ids.length; i++) {
                    $(`#${folder_ids[i]}button`).click(
                        () => {
                            $(`${folder_ids[i]}`).collapse("toggle")
                        }
                    )
                }
            }
        ).then(
            () => {
                $(".file-name").click(
                    event => {
                        add_file_to_selected(event.target.innerText)
                    }
                )
            }
        )
    }

const generate_folder_tree =
    (children, folder_ids) => {
        let html = ""
        if (children.length == 0) {
            return html
        }

        for (var i = 0; i < children.length; i++) {
            let current_item = children[i]
            if (current_item._children.length == 0) {
                html += file_template.replace("{to_be_replaced}", current_item._content["Name"])
            }
            else {
                let folder_name = current_item._content.Prefix.replace(new RegExp("/", 'g'), "-")
                folder_ids.push(folder_name)
                let dir_template_with_name = directory_template.replace(new RegExp("{directory-name}", 'g'), folder_name)
                folder_content = generate_folder_tree(current_item._children, folder_ids)
                let finished_dir_template = dir_template_with_name.replace("{folder-content}", folder_content)
                html += finished_dir_template
            }
        }
        return html
    }

const add_file_to_selected =
    filename => {
        if (selected_files.indexOf(filename) < 0) {
            $("#selected").append(selected_file_template.replace("{to_be_replaced}", filename))
            selected_files.push(filename)
            $(".selected-file").click(
                event => {
                    let filename = event.target.innerText
                    event.target.innerHTML = ""
                    selected_files = selected_files.filter(e => e !== filename)
                }
            )
        }
        else {
            return
        }
    }


const clear =
    selected_files => {
        selected_files = []
        $("#files").html("")

    }



let selected_files = []
$(document).ready(
    () => {
        let selected_container = $("#container-select").val()
        load_file_structure(selected_container)
        $("#container-select").on("change",
            event => {
                clear(selected_files)
                selected_container = event.target.value
                load_file_structure(selected_container)
            }
        )

        $("#submit-files").click(
            () => {
                data = {
                    "containerReferrence": selected_container,
                    "files": selected_files
                }
                console.log(data)
                let myHeaders = new Headers({ "content-type": "application/json" })
                fetch("/api/Generator",
                    {
                        method: "POST",
                        headers: myHeaders,
                        body: JSON.stringify(data)  
                    }
                ).then(
                    res => {
                        //return res.text()
                        return res.json()
                    }
                )
                .then(
                    json => {
                        //console.log(text)
                        let blob = new Blob([json.fileContent], {type: ""})
                        var url = window.URL.createObjectURL(blob)
                        var a = document.createElement('a')
                        a.href = url;
                        a.download = json.fileName;
                        document.body.appendChild(a); // we need to append the element to the dom -> otherwise it will not work in firefox
                        a.click();
                        a.remove();
                    }
                )
                //fetch("www.google.com").then(
                //    res => res.blob()
                //)
                //.then(
                //    blob => {
                //        var url = window.URL.createObjectURL(blob);
                //        var a = document.createElement('a');
                //        a.href = url;
                //        a.download = "google.html";
                //        document.body.appendChild(a); // we need to append the element to the dom -> otherwise it will not work in firefox
                //        a.click();
                //        a.remove();
                //    }
                //)
            }
        )


    }
)