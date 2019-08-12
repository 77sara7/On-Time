/*
(c) Copyright 2009 iOpus Software GmbH - http://www.iopus.com
*/

window.addEventListener("load", function (event) {
    TreeView.build();

    chrome.bookmarks.onChanged.addListener( function (id, x) {
        // TODO: listen to only iMacros descendants change
        window.location.reload();
    });
    chrome.bookmarks.onChildrenReordered.addListener( function (id, x) {
        // TODO: listen to only iMacros descendants change
        window.location.reload();
    });
    chrome.bookmarks.onCreated.addListener( function (id, x) {
        // TODO: listen to only iMacros descendants change
        window.location.reload();
    });
    chrome.bookmarks.onMoved.addListener( function (id, x) {
        // TODO: listen to only iMacros descendants change
        window.location.reload();
    });
    chrome.bookmarks.onRemoved.addListener( function (id, x) {
        // TODO: listen to only iMacros descendants change
        window.location.reload();
    });

    window.top.onSelectionChanged(TreeView.selectedItem != null);
}, true);


window.addEventListener("iMacrosRunMacro", function(evt) {
    document.getElementById("imacros-bookmark-div").setAttribute("name", evt.detail.name);
    document.getElementById("imacros-macro-container").value = evt.detail.source;
});

var TreeView = {
    // build tree from iMacros bookmarks folder
    build: function () {
        chrome.bookmarks.getTree( function (tree) {
            // first find iMacros subtree or create if not found
            // (code duplicates one in addToBookmarks(),
            // TODO: do something with that)
            var found = false,
                iMacrosFolderId = -1,
                bookmarksPanelId = tree[0].children[0].id;

            tree[0].children[0].children.forEach(function(child) {
                if (child.title == "iMacros") {
                    found = true;
                    iMacrosFolderId = child.id;
                }
            });

            if (!found) {
                chrome.bookmarks.create(
                    {
                        parentId: bookmarksPanelId,
                        title: "iMacros"
                    },
                    function (folder) {
                        iMacrosFolderId = folder.id;
                    }
                );
            }

            TreeView.buildSubTree_jstree(iMacrosFolderId);
        });
    },

    buildSubTree_jstree: function (id, parent) {
        if (!parent) {
            parent = document.getElementById("jstree");
        }

        chrome.bookmarks.getSubTree(id, function (treeNodes) {

            var traverseTree = function (node) {

                for (var i = node.length - 1; i >= 0; i--) {
                    var child = node[i];

                    if (child.children) {
                        traverseTree(child.children);
                    }

                    child.a_attr = new Object();

                    //Is current bookmark a bookmark or a folder
                    if (child.url) {
                        // skip non-macro bookmarks
                        if (!/iMacrosRunMacro/.test(child.url)) {
                            node.splice(i, 1);
                            continue;
                        }

                        child.type = 'macro';
                        child.a_attr.bookmarklet = child.url;
                    }
                    else {
                        child.type = 'folder';
                    }

                    child.text = child.title;
                    child.a_attr.bookmark_id = child.id;
                    child.a_attr.type = child.type;
                    
                }
            }

            traverseTree(treeNodes);
            
            if (!treeNodes[0].state) {
                treeNodes[0].state = new Object();
                treeNodes[0].state.opened = true;
            }

            function customMenu(node) {
                TreeView.selectedItem = node.original;

                var items = {
                    'Edit': {
                        'label': 'Edit',
                        'action': function () { window.top.edit(); }
                    },
                    'Convert': {
                        'label': 'Convert',
                        'action': function () { window.top.convert(); }
                    },
                    'New Folder': {
                        'label': 'New Folder',
                        'action': function () {
                            var new_name = prompt("Enter new folder name", "New folder");
                            var item = TreeView.selectedItem;

                            var root_id;

                            if (item.type == "folder") {
                                root_id = item.id;
                            }
                            else {
                                root_id = item.parentId;
                            }

                            chrome.bookmarks.getChildren(root_id, function (arr) {
                                // add ...(n) to the folder name if such name already present
                                var names = {}, count = 0, stop = false;
                                for (var i = 0; i < arr.length; i++) {
                                    names[arr[i].title] = true;
                                }
                                while (!stop && count < arr.length + 1) {
                                    if (names[new_name]) {
                                        count++;
                                        if (/\(\d+\)$/.test(new_name))
                                            new_name = new_name.replace(/\(\d+\)$/, "(" + count + ")");
                                        else
                                            new_name += " (" + count + ")";
                                    } else {
                                        stop = true;
                                    }
                                }
                                chrome.bookmarks.create(
                                    {
                                        parentId: root_id,
                                        title: new_name
                                    },
                                    function (folder) {
                                        TreeView.buildSubTree(folder.id);
                                    }
                                );
                            });
                        }
                    },
                    'Rename': {
                        'label': 'Rename',
                        'action': function () {
                            var item = TreeView.selectedItem;
                            if (!item) {
                                alert("Error: no item selected"); // should never happen
                                return;
                            }
                            var bookmark_id = item.id;
                            var old_name = item.text;
                            var new_name = prompt("Enter new name", old_name);
                            if (!new_name)
                                return;
                            if (item.type == "folder") {
                                chrome.bookmarks.update(bookmark_id, { title: new_name });
                            } else if (item.type == "macro") {
                                chrome.bookmarks.get(bookmark_id, function (x) {
                                    var url = x[0].url;
                                    // change macro name in URL
                                    try {
                                        var m = url.match(/, n = \"([^\"]+)\";/);
                                        url = url.replace(/, n = \"[^\"]+\";/,
                                            ", n = \"" + encodeURIComponent(new_name) + "\";"
                                        );
                                    } catch (e) {
                                        console.error(e);
                                    }
                                    chrome.bookmarks.update(
                                        bookmark_id, { title: new_name, url: url }
                                    );
                                });
                            }
                         }
                    },
                    'Remove': {
                        'label': 'Remove',
                        'action': function () {
                            var item = TreeView.selectedItem;
                            if (!item) {
                                alert("Error: no item selected");
                                return;
                            }
                            var bookmark_id = item.id;
                            if (!bookmark_id) {
                                alert("Can not delete " + item.type + " " + item.textContent);
                                return;
                            }

                            if (item.type == "macro") {
                                var yes = confirm("Are you sure you want to remove macro " +
                                                  item.title +
                                                  " ?");
                                if (yes) {
                                    chrome.bookmarks.remove(bookmark_id, function () {
                                        TreeView.selectedItem = null;
                                    });
                                }
                            } else if (item.type == "folder") {
                                var yes = confirm("Are you sure you want to remove folder " +
                                                  item.title +
                                                  " and all its contents?");
                                if (yes)
                                    chrome.bookmarks.removeTree(bookmark_id, function () {
                                        TreeView.selectedItem = null;
                                    });
                            }
                        }
                    },
                    'Refresh Tree': {
                        'label': 'Refresh Tree',
                        'action': function () { window.location.reload(); }
                    }
                }

                if (node.type === 'folder') {
                    delete items.Edit;
                    delete items.Convert;
                }

                return items;
            };

            jQuery('#jstree_container').jstree({
                'core': {

                    "check_callback": function (operation, node, parent, position, more) {
                        if (more.dnd && operation === "move_node") {
                            if(parent.id === "#") {
                                return false; // prevent moving a child above or below the root
                            }
                        }

                        return true; // allow everything else
                    },

                    'data': treeNodes
                },
                'types': {
                    'folder': {
                        
                    },
                    "macro": {
                        'icon': '/skin/imglog.png'
                    }
                },
                'contextmenu': {
                    'items': customMenu
                },
                'plugins': ['state', 'dnd', 'types', 'contextmenu', 'wholerow']
            });

            jQuery(document).on('dnd_stop.vakata', function (e, data) {
                var source = data.element;
                var target = data.event.target;

                if (target.getAttribute('type') == 'macro') {
                    chrome.bookmarks.get(target.getAttribute("bookmark_id"), function (a) {
                        chrome.bookmarks.move(
                            source.getAttribute("bookmark_id"),
                            { parentId: a[0].parentId, index: a[0].index }
                        );
                    });
                }
                else {
                    chrome.bookmarks.move(
                        source.getAttribute("bookmark_id"),
                        { parentId: target.getAttribute("bookmark_id"), index: 0 }
                    );
                }
            });

            jQuery('#jstree_container').on('select_node.jstree', function (e, data) {
                if (!(data.event && data.event.target)) {
                    return;
                }

                var element = data.event.target;

                TreeView.selectedItem = element;

                if (data.node.type == 'macro') {

                    TreeView.selectedItem.type = "macro";

                    var div = document.getElementById("imacros-bookmark-div");
                    if (div.hasAttribute("file_id"))
                        div.removeAttribute("file_id");
                    div.setAttribute("bookmark_id", element.getAttribute("bookmark_id"));
                    div.setAttribute("name", element.textContent);
                    var bookmarklet = element.getAttribute("bookmarklet");
                    var m = /var e_m64 = "([^"]+)"/.exec(bookmarklet);
                    if (!m) {
                        console.error("Can not parse bookmarklet " + element.textContent);
                        return;
                    }
                    document.getElementById("imacros-macro-container").value = decodeURIComponent(atob(m[1]));
                    window.top.onSelectionChanged(true);

                    e.preventDefault();
                    
                }
                //folder
                else {
                    TreeView.selectedItem.type = "folder";
                    window.top.onSelectionChanged(false);
                }
            });

            jQuery('#jstree_container').on('dblclick.jstree', function (e, data) {
                
                var target_node = jQuery('#jstree_container').jstree(true).get_node(e.target.getAttribute("bookmark_id"));
                
                if (target_node.type == 'macro') {
                    setTimeout(function () { window.top.play(); }, 200);
                }
            });
        });
    }
};
