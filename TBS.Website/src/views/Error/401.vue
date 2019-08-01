<template>
    <div class="text-center pt-5">
        <h1 class="text-danger pt-5">Oops! Error #401</h1>
        <h5 class="pt-4">You are unable to access this page as you do not have access. If you believe this is a mistake please contact [INSERT EMAIL]</h5>  <!-- TODO: Insert email -->
        <button class="btn btn-main btn bg-blue fade-on-hover text-uppercase text-white mt-3" @click="$router.go(-2)">Return</button>
    </div>
</template>