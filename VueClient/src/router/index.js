import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter);

const routes = [
	{
		path: '/',
		name: 'home',
		component: Home,
		meta: {
			title: route => "Home"
		}
	},
	{
		path: '/about',
		name: 'about',
		// route level code-splitting
		// this generates a separate chunk (about.[hash].js) for this route
		// which is lazy-loaded when the route is visited.
		component: () => import(/* webpackChunkName: "about" */ '../views/About.vue'),
		meta: {
			title: route => "About"
		}
	},
	{
		path: '/weather',
		name: 'weather',
		// route level code-splitting
		// this generates a separate chunk (about.[hash].js) for this route
		// which is lazy-loaded when the route is visited.
		component: () => import(/* webpackChunkName: "about" */ '../views/Weather.vue'),
		meta: {
			title: route => "Weather"
		}
	}
];

const router = new VueRouter({
	mode: 'history',
	base: process.env.BASE_URL,
	routes
});

router.afterEach((to/*, from*/) => {
	Vue.nextTick(() => {
		// Special environment symbol indicator (like Ð for development and Ŀ for local standalone)
		let title = process.env.VUE_APP_ENV_INDICATOR ? process.env.VUE_APP_ENV_INDICATOR + " " : "";
		// Route page title
		title += to.meta.title(to);
		// Global app title
		title += " | " + process.env.VUE_APP_TITLE;
		document.title = title;
	});
});

export default router;
