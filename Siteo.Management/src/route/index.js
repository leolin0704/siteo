import Router from 'vue-router';
export default new Router({
    routes: [
        {
            path: '/',
            redirect: '/login'
        },
        {
            path: '/login',
            component: resolve => require(['../page/login.vue'], resolve),
            meta: { title: 'Login' },
        },
        {
            path: '/',
            component: resolve => require(['../components/common/layout.vue'], resolve),
            meta: { title: '' },
            children:[
                {
                    path: '/home',
                    component: resolve => require(['../page/home.vue'], resolve),
                    meta: { title: 'Home Page' }
                },
                {
                    path: '/about',
                    component: resolve => require(['../page/about.vue'], resolve),
                    meta: { title: 'About Us' }
                },
                {
                    path: '/contact',
                    component: resolve => require(['../page/contact.vue'], resolve),
                    meta: { title: 'Contact Us' }
                },
                {
                    path: '/banner',
                    component: resolve => require(['../page/banner.vue'], resolve),
                    meta: { title: 'Banner Manage' }
                },
                {
                    path: '/news',
                    component: resolve => require(['../page/news.vue'], resolve),
                    meta: { title: 'Banner Manage' }
                },
                {
                    path: '/notice',
                    component: resolve => require(['../page/notice.vue'], resolve),
                    meta: { title: 'Banner Manage' }
                },
                {
                    path: '/user',
                    component: resolve => require(['../page/user.vue'], resolve),
                    meta: { title: 'Banner Manage' }
                },
                {
                    path: '/role',
                    component: resolve => require(['../page/role.vue'], resolve),
                    meta: { title: 'Banner Manage' }
                }
            ]
        }
    ]
})
