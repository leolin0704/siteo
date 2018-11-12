export default [
    {
        key:"Home",
        name:"Home",
        url:"/home"
    },
    {
        key:"Banner",
        name:"Banner",
        url:"/banner"
    },
    {
        key:"About",
        name:"About Us",
        url:"/about"
    },
    {
        key:"Contact",
        name:"Contact Us",
        url:"/contact"
    },
    {
        key:"System",
        name:"System Manage",
        url:"/notice",
        subModules:[
            {
                key:"Notice",
                name:"Notice Manage",
                url:"/notice"
            },
            {
                key:"user",
                name:"User Manage",
                url:"/user"
            },
            {
                key:"role",
                name:"Role Manage",
                url:"/role"
            }
        ]
    },
]