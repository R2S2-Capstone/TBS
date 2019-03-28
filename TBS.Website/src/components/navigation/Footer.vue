<template>
    <footer class="fixed-bottom bg-blue">
        <div class="text-center p-3">© {{ new Date().getFullYear() }} Copyright:
            <router-link :to="{ name: home }" class="text-white fade-on-hover"> TBS</router-link>
        </div>
    </footer>
</template>