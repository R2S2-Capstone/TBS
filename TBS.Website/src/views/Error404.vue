<template>
    <div class="text-center pt-5">
        <h1 class="text-danger pt-5">Oops! Error #404</h1>
        <h5 class="pt-4">We are unable to locate the page you're looking for</h5>
        <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white mt-3" @click="$router.go(-1)">Return</button>
    </div>
</template>