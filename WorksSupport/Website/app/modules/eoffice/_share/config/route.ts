let route = {
    // Loại Dự Án route
    project:
    {
        projects: { name: "Projects", path: "/loai-du-an" },
        addProject: { name: "AddProject", path: "/them-moi-loai-du-an" }
    },
    worksgroup:
    {
        worksgroups: { name: "WorksGroups", path: "/nhom-cong-viec" },
        addworksgroup: { name: "AddWorksGroup", path: "/them-nhom-cong-viec" }
    },

    work:
    {
        works: { name: "Works", path: "/cong-viec" },
    }
};
export default route;